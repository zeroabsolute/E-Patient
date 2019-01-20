using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Config;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Detyra___EPacient.Models {
    class User {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        private string password;

        public User() {
            this.Id = 0;
            this.Role = null;
            this.Email = null;
            this.password = null;
        }

        public User(int id, Role role, string email, string password) {
            this.Id = id;
            this.Role = role;
            this.Email = email;
            this.password = password;
        }

        /**
         * To string
         */
        
        public string toString() {
            return $@"
                User Attributes:
                
                Id: {this.Id}
                Role Id: {this.Role.Id}
                Role: {this.Role.Name}
                Email: {this.Email}";
        }

        /**
         * Business logic
         * Validations
         * Database connection for the following:
         * 1) CREATE User object in the database
         * 2) READ User object in the database
         * 3) UPDATE User object in the database
         * 4) LOGIN
         */

        /* Log in */

        public async Task userLogIn(string email, string password) {
            try {
                bool emailIsValid = Validators.validateEmail(email);
                bool passwordIsValid = Validators.validatePassword(password);

                if (emailIsValid && passwordIsValid) {
                    // Check if user with the given email exists
                    string query = $@"
                        SELECT
                            user.id as userId,
                            user.email,
                            user.password,
                            role.id as roleId,
                            role.name as role
                        FROM
                            {DBTables.USER} as user
                            INNER JOIN
                            {DBTables.ROLE} as role
                            ON
                            {DBTables.USER}.role = {DBTables.ROLE}.id
                        WHERE
                            user.email = @email";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Prepare();

                    DbDataReader reader = await cmd.ExecuteReaderAsync();
                    int userId = -1, roleId = -1;
                    string userEmail = "", userPassword = "", role = "";

                    while(reader.Read()) {
                        userId = reader.GetInt32(reader.GetOrdinal("userId"));
                        roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                        userEmail = reader.GetString(reader.GetOrdinal("email"));
                        userPassword = reader.GetString(reader.GetOrdinal("password"));
                        role = reader.GetString(reader.GetOrdinal("role"));

                        break;
                    }

                    if (userId == -1) {
                        throw new Exception("Përdoruesi me email-in e dhënë nuk ekziston");
                    }

                    // User with the given email exists => check if passwords match
                    PasswordHash passwordHash = new PasswordHash(password);
                    bool passwordsMatch = passwordHash.verify(userPassword, password);

                    if (!passwordsMatch) {
                        throw new Exception("Fjalëkalimi i vendosur është i gabuar");
                    }

                    // Password is correct => initialize user object
                    this.Id = userId;
                    this.Role = new Role(roleId, role);
                    this.Email = email;

                    reader.Close();
                    connection.Close();
                } else {
                    throw new Exception("Format i gabuar i email-it ose fjalëkalimit");
                }
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Config;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;

namespace Detyra___EPacient.Models {
    class User {
        private int id;
        private int roleId;
        private string role;
        private string email;
        private string password;

        public User() {
            this.id = 0;
            this.roleId = 0;
            this.role = null;
            this.email = null;
            this.password = null;
        }

        public User(int id, int roleId, string role, string email, string password) {
            this.id = id;
            this.roleId = roleId;
            this.role = role;
            this.email = email;
            this.password = password;
        }

        /**
         * To string
         */
        
        public string toString() {
            return $@"
                User Attributes:
                
                Id: {this.id}
                Role Id: {this.roleId}
                Role: {this.role}
                Email: {this.email}";
        }

        /**
         * Getters and setters
         */

        // ID
        public void setId(int id) {
            if (id != 0) {
                this.id = id;
            }
        }

        public int getId() {
            return this.id;
        }

        // Role
        public void setRole(string role) {
            if (role != null) {
                this.role = role;
            }
        }

        public string getRole() {
            return this.role;
        }

        // Email
        public void setEmail(string email) {
            if (email != null) {
                this.email = email;
            }
        }

        public string getEmail() {
            return this.email;
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

        public User userLogIn(string email, string password) {
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

                    MySqlDataReader reader = cmd.ExecuteReader();
                    int userId = -1, roleId = -1;
                    string userEmail = "", userPassword = "", role = "";

                    while(reader.Read()) {
                        userId = reader.GetInt32("userId");
                        roleId = reader.GetInt32("roleId");
                        userEmail = reader.GetString("email");
                        userPassword = reader.GetString("password");
                        role = reader.GetString("role");

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

                    // Password is correct => create a user object and return it
                    User loggedInUser = new User(userId, roleId, role, userEmail, userPassword);

                    reader.Close();
                    connection.Close();
                    return loggedInUser;
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

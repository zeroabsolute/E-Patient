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
    public class User {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        private string password;

        public User() {
            this.Id = 0;
            this.Role = null;
            this.Status = Statuses.INACTIVE.Id;
            this.Email = null;
            this.password = null;
        }

        public User(int id, Role role, int status, string email, string password) {
            this.Id = id;
            this.Role = role;
            this.Status = status;
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
                            user.status,
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
                    int userId = -1, roleId = -1, userStatus = -1;
                    string userEmail = "", userPassword = "", role = "";

                    while(reader.Read()) {
                        userId = reader.GetInt32(reader.GetOrdinal("userId"));
                        roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                        userEmail = reader.GetString(reader.GetOrdinal("email"));
                        userPassword = reader.GetString(reader.GetOrdinal("password"));
                        userStatus = reader.GetInt32(reader.GetOrdinal("status"));
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

                    // Check if user is activated
                    if (userStatus == Statuses.INACTIVE.Id) {
                        throw new Exception("Llogaria juaj është inaktive");
                    }

                    // Initialize user object
                    this.Id = userId;
                    this.Role = new Role(roleId, role);
                    this.Email = email;
                    this.Status = userStatus;

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

        /* Create user */

        public async Task<long> createUser(string email, string password, int role) {
            try {
                bool emailIsValid = Validators.validateEmail(email);
                bool passwordIsValid = Validators.validatePassword(password);
                bool roleIsValid = role != -1;

                if (emailIsValid && passwordIsValid && roleIsValid) {
                    PasswordHash passwordHash = new PasswordHash(password);
                    string hashedPassword = passwordHash.toString();
                    string query = $@"
                        INSERT INTO
                            {DBTables.USER}
                        VALUES (
                            null,
                            @email,
                            @hashedPassword,
                            @role,
                            @status
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
                    cmd.Prepare();

                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                    return cmd.LastInsertedId;
                } else {
                    throw new Exception("Format i gabuar i email-it ose fjalëkalimit");
                }
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }

        /**
         * Archive or activate a user.
         */

        public async Task<Status> toggleArchive(int userId, int currentStatus) {
            try {
                string query = $@"
                        UPDATE
                            {DBTables.USER}
                        SET
                            status = @currentStatus
                        WHERE
                            {DBTables.USER}.id = @id";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                Status newStatus = currentStatus == Statuses.ACTIVE.Id ? Statuses.INACTIVE : Statuses.ACTIVE;

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.Parameters.AddWithValue("@currentStatus", newStatus.Id);
                cmd.Prepare();

                await cmd.ExecuteNonQueryAsync();

                connection.Close();

                return newStatus;
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }
    }
}

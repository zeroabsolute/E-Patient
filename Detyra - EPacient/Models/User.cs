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
        private string role;
        private string email;
        private string password;

        public User() {
            this.id = 0;
            this.role = null;
            this.email = null;
            this.password = null;
        }

        public User(int id, string role, string email, string password) {
            this.id = id;
            this.role = role;
            this.email = email;
            this.password = password;
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

        public void userLogIn(string email, string password) {
            try {
                bool emailIsValid = Validators.validateEmail(email);
                bool passwordIsValid = Validators.validatePassword(password);
                PasswordHash hashedPassword = new PasswordHash(password);
                
                string hashedPasswordString = hashedPassword.getHashedPassword();

                bool res = hashedPassword.verify(password);
                Console.WriteLine(res);

                /*if (emailIsValid && passwordIsValid) {
                    string query = $@"
                        SELECT
                            user.id as userId
                            user.email
                            role.id as roleId
                            role.name as role
                        FROM
                            {DBTables.USER} as user
                            INNER JOIN
                            {DBTables.ROLE} as role
                        WHERE
                            user.email = {email}
                            AND
                            user.password = {hashedPasswordString}
                    ";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();


                } else {
                    throw new Exception("Format i gabuar i email-it ose fjalëkalimit");
                }*/
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }
    }
}

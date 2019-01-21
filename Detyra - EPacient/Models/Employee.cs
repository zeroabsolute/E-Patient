using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Config;

namespace Detyra___EPacient.Models {
    class Employee {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }

        public Employee() {
            this.Id = -1;
            this.FirstName = "";
            this.LastName = "";
            this.PhoneNumber = "";
            this.Address = "";
            this.DateOfBirth = new DateTime();
            this.User = null;
        }

        public Employee(
            int id,
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            DateTime dob,
            User user
        ) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateOfBirth = dob;
            this.User = user;
        }

        /* Create employee */

        public async Task<long> createEmployee(
            string firstName, 
            string lastName, 
            string phoneNumber,
            string address,
            string dateOfBirth,
            long user
        ) {
            try {
                bool firstNameIsValid = firstName != null && firstName.Length > 0;
                bool lastNameIsValid = lastName != null && lastName.Length > 0;
                bool phoneNumberIsValid = phoneNumber != null && phoneNumber.Length > 0;
                bool dateOfBirthIsValid = dateOfBirth != null && dateOfBirth.Length > 0;
                bool userIdValid = user != -1;

                if (firstNameIsValid && lastNameIsValid && phoneNumberIsValid && dateOfBirthIsValid && userIdValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.EMPLOYEE}
                        VALUES (
                            null,
                            @firstName,
                            @lastName,
                            @phoneNumber,
                            @address,
                            @dateOfBirth,
                            @user
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Prepare();

                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                    return cmd.LastInsertedId;
                } else {
                    throw new Exception("Input i gabuar ose i pamjaftueshëm");
                }
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }
    }
}

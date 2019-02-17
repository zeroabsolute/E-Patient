using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Config;
using System.Data.Common;

namespace Detyra___EPacient.Models {
    public class Employee {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }

        public Employee() {
            this.Id = -1;
            this.FirstName = "";
            this.LastName = "";
            this.FullName = "";
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
            this.FullName = $"{firstName} {lastName}";
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateOfBirth = dob;
            this.User = user;
        }

        /**
         * Method to read employees from the database
         */

        public async Task<List<Employee>> readEmployees() {
            try {
                string query = $@"
                        SELECT
                            role.id as roleId,
                            role.name as roleName,
                            user.id as userId,
                            user.email as userEmail,
                            user.status as userStatus,
                            employee.id as employeeId,
                            employee.first_name as firstName,
                            employee.last_name as lastName,
                            employee.phone_number as phoneNumber,
                            employee.address as address,
                            employee.date_of_birth as dateOfBirth
                        FROM 
                            {DBTables.EMPLOYEE} as employee
                            INNER JOIN
                                {DBTables.USER} as user
                                ON
                                {DBTables.EMPLOYEE}.user = {DBTables.USER}.id
                            INNER JOIN
                                {DBTables.ROLE} as role
                                ON
                                {DBTables.USER}.role = {DBTables.ROLE}.id";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Employee> employees = new List<Employee>();

                while (reader.Read()) {
                    int roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                    string roleName = reader.GetString(reader.GetOrdinal("roleName"));
                    int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                    int userStatus = reader.GetInt32(reader.GetOrdinal("userStatus"));
                    string userEmail = reader.GetString(reader.GetOrdinal("userEmail"));
                    int employeeId = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string firstName = reader.GetString(reader.GetOrdinal("firstName"));
                    string lastName = reader.GetString(reader.GetOrdinal("lastName"));
                    DateTime dob = reader.GetDateTime(reader.GetOrdinal("dateOfBirth"));
                    string phoneNumber = reader.GetString(reader.GetOrdinal("phoneNumber"));
                    string address = reader.GetString(reader.GetOrdinal("address"));

                    Role currentRole = new Role(roleId, roleName);
                    User currentUser = new User(userId, currentRole, userStatus, userEmail, null);
                    Employee currentEmployee = new Employee(
                        employeeId,
                        firstName,
                        lastName,
                        phoneNumber,
                        address,
                        dob,
                        currentUser
                    );

                    employees.Add(currentEmployee);
                }

                return employees;
            } catch (Exception e) {
                throw e;
            }
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
                            @user,
                            @status
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
                    cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
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

        /* Get one employee by user id */

        public async Task<int> getEmployeeByUserId(int userId) {
            try {
                string query = $@"
                        SELECT *
                        FROM 
                            {DBTables.EMPLOYEE}
                        WHERE
                            {DBTables.EMPLOYEE}.user = @userId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read()) {
                    return reader.GetInt32(reader.GetOrdinal("id"));
                }

                return -1;
            } catch (Exception e) {
                return -1;
            }
        }
    }
}

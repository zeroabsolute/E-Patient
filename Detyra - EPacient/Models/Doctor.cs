using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;

namespace Detyra___EPacient.Models {
    class Doctor {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public string SpecializedIn { get; set; }

        public Doctor() {
            this.Id = -1;
            this.Employee = null;
            this.SpecializedIn = "";
        }

        public Doctor(
            int id,
            Employee employee,
            string specializedIn
        ) {
            this.Id = id;
            this.Employee = employee;
            this.SpecializedIn = specializedIn;
        }

        /**
         * Method to read doctors from the database
         */
        
        public async Task<List<Doctor>> readDoctors() {
            try {
                string query = $@"
                        SELECT
                            role.id as roleId,
                            role.name as roleName,
                            user.id as userId,
                            user.email as userEmail,
                            employee.id as employeeId,
                            employee.first_name as employeeFirstName,
                            employee.last_name as employeeLastName,
                            employee.phone_number as employeePhoneNumber,
                            employee.address as employeeAddress,
                            employee.date_of_birth as employeeDateOfBirth,
                            doctor.id as doctorId,
                            doctor.specialized_in as doctorSpecialization
                        FROM 
                            {DBTables.DOCTOR} as doctor
                            INNER JOIN
                                {DBTables.EMPLOYEE} as employee
                                ON
                                {DBTables.DOCTOR}.employee = {DBTables.EMPLOYEE}.id
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
                List<Doctor> doctors = new List<Doctor>();

                while (reader.Read()) {
                    int roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                    string roleName = reader.GetString(reader.GetOrdinal("roleName"));
                    int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                    string userEmail = reader.GetString(reader.GetOrdinal("userEmail"));
                    int employeeId = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string employeeFirstName = reader.GetString(reader.GetOrdinal("employeeFirstName"));
                    string employeeLastName = reader.GetString(reader.GetOrdinal("employeeLastName"));
                    string employeePhoneNumber = reader.GetString(reader.GetOrdinal("employeePhoneNumber"));
                    string employeeAddress = reader.GetString(reader.GetOrdinal("employeeAddress"));
                    DateTime employeeDOB = reader.GetDateTime(reader.GetOrdinal("employeeDateOfBirth"));
                    int doctorId = reader.GetInt32(reader.GetOrdinal("doctorId"));
                    string doctorSpecialization = reader.GetString(reader.GetOrdinal("doctorSpecialization"));

                    Role currentRole = new Role(roleId, roleName);
                    User currentUser = new User(userId, currentRole, userEmail, null);
                    Employee currentEmployee = new Employee(
                        employeeId,
                        employeeFirstName,
                        employeeLastName,
                        employeePhoneNumber,
                        employeeAddress,
                        employeeDOB,
                        currentUser
                    );
                    Doctor currentDoctor = new Doctor(doctorId, currentEmployee, doctorSpecialization);

                    doctors.Add(currentDoctor);
                }

                return doctors;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

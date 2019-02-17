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
    public class Doctor {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Employee Employee { get; set; }
        public string SpecializedIn { get; set; }

        public Doctor() {
            this.Id = -1;
            this.Employee = null;
            this.SpecializedIn = "";
            this.FullName = "";
        }

        public Doctor(
            int id,
            Employee employee,
            string specializedIn
        ) {
            this.Id = id;
            this.Employee = employee;
            this.SpecializedIn = specializedIn;
            this.FullName = employee.FullName;
        }

        /**
         * Method to read doctors from the database
         */

        // All doctors
        
        public async Task<List<Doctor>> readDoctors() {
            try {
                string query = $@"
                        SELECT
                            role.id as roleId,
                            role.name as roleName,
                            user.id as userId,
                            user.email as userEmail,
                            user.status as userStatus,
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
                    int userStatus = reader.GetInt32(reader.GetOrdinal("userStatus"));
                    int employeeId = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string employeeFirstName = reader.GetString(reader.GetOrdinal("employeeFirstName"));
                    string employeeLastName = reader.GetString(reader.GetOrdinal("employeeLastName"));
                    string employeePhoneNumber = reader.GetString(reader.GetOrdinal("employeePhoneNumber"));
                    string employeeAddress = reader.GetString(reader.GetOrdinal("employeeAddress"));
                    DateTime employeeDOB = reader.GetDateTime(reader.GetOrdinal("employeeDateOfBirth"));
                    int doctorId = reader.GetInt32(reader.GetOrdinal("doctorId"));
                    string doctorSpecialization = reader.GetString(reader.GetOrdinal("doctorSpecialization"));

                    Role currentRole = new Role(roleId, roleName);
                    User currentUser = new User(userId, currentRole, userStatus, userEmail, null);
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

        // Filtered by sector

        public async Task<List<Doctor>> readDoctors(int sectorId) {
            try {
                string query = $@"
                        SELECT
                            role.id as roleId,
                            role.name as roleName,
                            user.id as userId,
                            user.email as userEmail,
                            user.status as userStatus,
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
                                {DBTables.USER}.role = {DBTables.ROLE}.id
                        WHERE
                            doctor.specialized_in = @sectorId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@sectorId", sectorId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Doctor> doctors = new List<Doctor>();

                while (reader.Read()) {
                    int roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                    string roleName = reader.GetString(reader.GetOrdinal("roleName"));
                    int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                    string userEmail = reader.GetString(reader.GetOrdinal("userEmail"));
                    int userStatus = reader.GetInt32(reader.GetOrdinal("userStatus"));
                    int employeeId = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string employeeFirstName = reader.GetString(reader.GetOrdinal("employeeFirstName"));
                    string employeeLastName = reader.GetString(reader.GetOrdinal("employeeLastName"));
                    string employeePhoneNumber = reader.GetString(reader.GetOrdinal("employeePhoneNumber"));
                    string employeeAddress = reader.GetString(reader.GetOrdinal("employeeAddress"));
                    DateTime employeeDOB = reader.GetDateTime(reader.GetOrdinal("employeeDateOfBirth"));
                    int doctorId = reader.GetInt32(reader.GetOrdinal("doctorId"));
                    string doctorSpecialization = reader.GetString(reader.GetOrdinal("doctorSpecialization"));

                    Role currentRole = new Role(roleId, roleName);
                    User currentUser = new User(userId, currentRole, userStatus, userEmail, null);
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

        /* Create doctor */

        public async Task<long> createDoctor(int specialization, long employee) {
            try {
                bool specializationIsValid = specialization > 0;
                bool employeeIsValid = employee != -1;

                if (employeeIsValid && specializationIsValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.DOCTOR}
                        VALUES (
                            null,
                            @employee,
                            @specializedIn,
                            @status
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@specializedIn", specialization);
                    cmd.Parameters.AddWithValue("@employee", employee);
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

        /*
         * Get total number of doctors
         */

        public async Task<int> getDoctorsCount() {
            try {
                string query = $@"
                    SELECT
                        COUNT(*)
                    FROM 
                        {DBTables.DOCTOR}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            } catch (Exception e) {
                throw e;
            }
        }

        /* Get one doctor by user id */

        public async Task<int> getDoctorByUserId(int userId) {
            try {
                string query = $@"
                        SELECT
                            doctor.id as doctorId
                        FROM 
                            {DBTables.DOCTOR} as doctor
                            INNER JOIN
                            {DBTables.EMPLOYEE}
                            ON
                            {DBTables.DOCTOR}.employee = {DBTables.EMPLOYEE}.id
                        WHERE
                            {DBTables.EMPLOYEE}.user = @userId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read()) {
                    return reader.GetInt32(reader.GetOrdinal("doctorId"));
                }

                return -1;
            } catch (Exception e) {
                return -1;
            }
        }
    }
}

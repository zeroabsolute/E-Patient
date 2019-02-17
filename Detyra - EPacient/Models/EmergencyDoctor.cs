using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Detyra___EPacient.Models {
    public class EmergencyDoctor {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }

        public EmergencyDoctor() {
            this.Id = 0;
            this.Doctor = null;
            this.Date = DateTime.Now;
        }

        public EmergencyDoctor(
            int id,
            Doctor doctor,
            DateTime date
        ) {
            this.Id = id;
            this.Doctor = doctor;
            this.Date = date;
        }

        /* Read all emergency doctors for one month and one sector */

        // Filtered by sector
        public async Task<List<EmergencyDoctor>> readEmergencyDoctors(int sector) {
            try {
                string query = $@"
                        SELECT
                            emergencyDoc.id as emergencyDocId,
                            emergencyDoc.date as emergencyDocDate,
                            employee.id as employeeId,
                            employee.first_name as employeeFirstName,
                            employee.last_name as employeeLastName,
                            employee.phone_number as employeePhoneNumber,
                            employee.address as employeeAddress,
                            employee.date_of_birth as employeeDateOfBirth,
                            doctor.id as doctorId,
                            doctor.specialized_in as doctorSpecialization
                        FROM 
                            {DBTables.EMERGENCY_DOCTOR} as emergencyDoc
                            INNER JOIN
                                {DBTables.DOCTOR} as doctor
                                ON
                                emergencyDoc.doctor = {DBTables.DOCTOR}.id
                            INNER JOIN
                                {DBTables.EMPLOYEE} as employee
                                ON
                                {DBTables.DOCTOR}.employee = {DBTables.EMPLOYEE}.id
                        WHERE
                            doctor.specialized_in = @sectorId
                        ORDER BY
                            emergencyDocDate
                            DESC";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@sectorId", sector);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<EmergencyDoctor> emergencyDoctors = new List<EmergencyDoctor>();

                while (reader.Read()) {
                    int employeeId = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string employeeFirstName = reader.GetString(reader.GetOrdinal("employeeFirstName"));
                    string employeeLastName = reader.GetString(reader.GetOrdinal("employeeLastName"));
                    string employeePhoneNumber = reader.GetString(reader.GetOrdinal("employeePhoneNumber"));
                    string employeeAddress = reader.GetString(reader.GetOrdinal("employeeAddress"));
                    DateTime employeeDOB = reader.GetDateTime(reader.GetOrdinal("employeeDateOfBirth"));
                    int doctorId = reader.GetInt32(reader.GetOrdinal("doctorId"));
                    string doctorSpecialization = reader.GetString(reader.GetOrdinal("doctorSpecialization"));

                    Employee currentEmployee = new Employee(
                        employeeId,
                        employeeFirstName,
                        employeeLastName,
                        employeePhoneNumber,
                        employeeAddress,
                        employeeDOB,
                        null
                    );
                    Doctor currentDoctor = new Doctor(doctorId, currentEmployee, doctorSpecialization);
                    EmergencyDoctor currentEmergencyDoctor = new EmergencyDoctor(
                        reader.GetInt32(reader.GetOrdinal("emergencyDocId")),
                        currentDoctor,
                        reader.GetDateTime(reader.GetOrdinal("emergencyDocDate"))
                    );

                    emergencyDoctors.Add(currentEmergencyDoctor);
                }

                return emergencyDoctors;
            } catch (Exception e) {
                throw e;
            }
        }

        // Filtered by sector, month, year
        public async Task<List<EmergencyDoctor>> readEmergencyDoctors(int sector, string month, string year) {
            try {
                string query = $@"
                        SELECT
                            emergencyDoc.id as emergencyDocId,
                            emergencyDoc.date as emergencyDocDate,
                            employee.id as employeeId,
                            employee.first_name as employeeFirstName,
                            employee.last_name as employeeLastName,
                            employee.phone_number as employeePhoneNumber,
                            employee.address as employeeAddress,
                            employee.date_of_birth as employeeDateOfBirth,
                            doctor.id as doctorId,
                            doctor.specialized_in as doctorSpecialization
                        FROM 
                            {DBTables.EMERGENCY_DOCTOR} as emergencyDoc
                            INNER JOIN
                                {DBTables.DOCTOR} as doctor
                                ON
                                emergencyDoc.doctor = {DBTables.DOCTOR}.id
                            INNER JOIN
                                {DBTables.EMPLOYEE} as employee
                                ON
                                {DBTables.DOCTOR}.employee = {DBTables.EMPLOYEE}.id
                        WHERE
                            MONTH(emergencyDoc.date) = @month
                            AND
                            YEAR(emergencyDoc.date) = @year
                            AND
                            doctor.specialized_in = @sectorId
                        ORDER BY
                            emergencyDocDate
                            DESC";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@sectorId", sector);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<EmergencyDoctor> emergencyDoctors = new List<EmergencyDoctor>();

                while (reader.Read()) {
                    int employeeId = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string employeeFirstName = reader.GetString(reader.GetOrdinal("employeeFirstName"));
                    string employeeLastName = reader.GetString(reader.GetOrdinal("employeeLastName"));
                    string employeePhoneNumber = reader.GetString(reader.GetOrdinal("employeePhoneNumber"));
                    string employeeAddress = reader.GetString(reader.GetOrdinal("employeeAddress"));
                    DateTime employeeDOB = reader.GetDateTime(reader.GetOrdinal("employeeDateOfBirth"));
                    int doctorId = reader.GetInt32(reader.GetOrdinal("doctorId"));
                    string doctorSpecialization = reader.GetString(reader.GetOrdinal("doctorSpecialization"));

                    Employee currentEmployee = new Employee(
                        employeeId,
                        employeeFirstName,
                        employeeLastName,
                        employeePhoneNumber,
                        employeeAddress,
                        employeeDOB,
                        null
                    );
                    Doctor currentDoctor = new Doctor(doctorId, currentEmployee, doctorSpecialization);
                    EmergencyDoctor currentEmergencyDoctor = new EmergencyDoctor(
                        reader.GetInt32(reader.GetOrdinal("emergencyDocId")),
                        currentDoctor,
                        reader.GetDateTime(reader.GetOrdinal("emergencyDocDate"))
                    );

                    emergencyDoctors.Add(currentEmergencyDoctor);
                }

                return emergencyDoctors;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create emergency doctor */

        public async Task<long> createEmergencyDoctor(string date, int doctor) {
            string query = $@"
                        INSERT INTO
                            {DBTables.EMERGENCY_DOCTOR}
                        VALUES (
                            null,
                            @doctor,
                            @date,
                            @status
                        )";

            MySqlConnection connection = new MySqlConnection(DB.connectionString);
            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@doctor", doctor);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
            cmd.Prepare();

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
            return cmd.LastInsertedId;
        }

        /**
         * Generate emergency doctors.
         * Algorithm: Round-robin
         */

        public async Task generateEmergencyDoctors(int sector, string month, string year) {
            try {
                if (sector < 1 && month == null) {
                    throw new Exception("Input i gabuar ose i pamjaftueshëm");
                }

                List<Doctor> doctors = await new Doctor().readDoctors(sector);
                List<EmergencyDoctor> data = await this.readEmergencyDoctors(sector);
                List<EmergencyDoctor> dataForSelectedMonth = await this.readEmergencyDoctors(sector, month, year);
                int monthNum = int.Parse(month);
                int yearNum = int.Parse(year);
                int daysInMonth = DateTime.DaysInMonth(yearNum, monthNum);

                /**
                 * Check data length.
                 * If there is data => get last doctor id and add data.
                 * If not, start by first doctor in line (by their ID).
                 */

                int lastId = -1;
                int index = -1;

                // Return if there is data for current sector, month, year
                if (dataForSelectedMonth.Count > 0) {
                    throw new Exception("Mjekët roje janë caktuar për muajin, vitin dhe sektorin e zgjedhur");
                }

                // Return if there are no doctors
                if (doctors.Count == 0) {
                    throw new Exception("Nuk ka mjekë të disponueshëm për sektorin e zgjedhur");
                }

                // Next index calculation
                if (data.Count > 0) {
                    lastId = data[0].Doctor.Id;
                }

                if (lastId != -1) {
                    index = doctors.IndexOf(doctors.Where(item => item.Id == lastId).FirstOrDefault());
                } else {
                    index = 0;
                }

                if (index + 1 == doctors.Count) {
                    index = 0;
                } else {
                    index += 1;
                }

                // Generate
                for (int i = 1; i <= daysInMonth; i += 1) {
                    string date = $"{year}-{month}-{i}";
                    int doctorId = doctors[index++].Id;

                    await new EmergencyDoctor().createEmergencyDoctor(
                        date,
                        doctorId
                    );

                    if (index == doctors.Count) {
                        index = 0;
                    }
                }
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

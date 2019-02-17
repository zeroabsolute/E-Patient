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
    public class Patient {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public Patient() {
            this.Id = -1;
            this.FirstName = "";
            this.LastName = "";
            this.FullName = "";
            this.PhoneNumber = "";
            this.Address = "";
            this.DateOfBirth = new DateTime();
            this.Gender = "";
        }

        public Patient(
            int id,
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            DateTime dob,
            string gender
        ) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = $"{firstName} {lastName}";
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateOfBirth = dob;
            this.Gender = gender;
        }

        /**
         * Method to read patients from the database
         */

        public async Task<List<Patient>> readPatients() {
            try {
                string query = $@"
                        SELECT *
                        FROM {DBTables.PATIENT}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Patient> patients = new List<Patient>();

                while (reader.Read()) {
                    Patient currentPatient = new Patient(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("first_name")),
                        reader.GetString(reader.GetOrdinal("last_name")),
                        reader.GetString(reader.GetOrdinal("phone_number")),
                        reader.GetString(reader.GetOrdinal("address")),
                        reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                        reader.GetString(reader.GetOrdinal("gender"))
                    );

                    patients.Add(currentPatient);
                }

                return patients;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create patient */

        public async Task<long> createPatient(
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            string dateOfBirth,
            string gender
        ) {
            try {
                bool firstNameIsValid = firstName != null && firstName.Length > 0;
                bool lastNameIsValid = lastName != null && lastName.Length > 0;
                bool phoneNumberIsValid = phoneNumber != null && phoneNumber.Length > 0;
                bool dateOfBirthIsValid = dateOfBirth != null && dateOfBirth.Length > 0;
                bool genderIsValid = dateOfBirth != null && dateOfBirth.Length > 0;

                if (firstNameIsValid && lastNameIsValid && phoneNumberIsValid && dateOfBirthIsValid && genderIsValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.PATIENT}
                        VALUES (
                            null,
                            @firstName,
                            @lastName,
                            @dateOfBirth,
                            @gender,
                            @phoneNumber,
                            @address,
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
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
                    cmd.Prepare();

                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                    long patientId = cmd.LastInsertedId;

                    await new PatientChart().createPatientChart(
                        DateTime.Now.ToString(DateTimeFormats.MYSQL_DATE), 
                        patientId
                    );

                    return patientId;
                } else {
                    throw new Exception("Input i gabuar ose i pamjaftueshëm");
                }
            } catch (Exception e) {
                throw e;
            }
        }

        /* Update patient */

        public async Task<long> updatePatient(
            long id,
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            string dateOfBirth,
            string gender
        ) {
            try {
                bool idIsValid = id != -1;
                bool firstNameIsValid = firstName != null && firstName.Length > 0;
                bool lastNameIsValid = lastName != null && lastName.Length > 0;
                bool phoneNumberIsValid = phoneNumber != null && phoneNumber.Length > 0;
                bool dateOfBirthIsValid = dateOfBirth != null && dateOfBirth.Length > 0;
                bool genderIsValid = dateOfBirth != null && dateOfBirth.Length > 0;

                if (
                    idIsValid && 
                    firstNameIsValid && 
                    lastNameIsValid && 
                    phoneNumberIsValid && 
                    dateOfBirthIsValid && 
                    genderIsValid
                ) {
                    string query = $@"
                        UPDATE
                            {DBTables.PATIENT}
                        SET
                            first_name = @firstName,
                            last_name = @lastName,
                            date_of_birth = @dateOfBirth,
                            gender = @gender,
                            phone_number = @phoneNumber,
                            address = @address
                        WHERE
                            {DBTables.PATIENT}.id = @id";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Prepare();

                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                    return cmd.LastInsertedId;
                } else {
                    throw new Exception("Input i gabuar ose i pamjaftueshëm");
                }
            } catch (Exception e) {
                throw e;
            }
        }

        /*
         * Get total number of patients
         */

        public async Task<int> getPatientsCount() {
            try {
                string query = $@"
                    SELECT
                        COUNT(*)
                    FROM 
                        {DBTables.PATIENT}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

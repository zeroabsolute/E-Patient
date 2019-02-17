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
    public class PatientChart {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Patient Patient { get; set; }

        public PatientChart() {
            this.Id = 0;
            this.CreatedAt = DateTime.Now;
            this.Patient = null;
        }

        public PatientChart(int id, DateTime createdAt, Patient patient) {
            this.Id = id;
            this.CreatedAt = createdAt;
            this.Patient = patient;
        }

        /* Create patient chart */

        public async Task<long> createPatientChart(string createdAt, long patientId) {
            try {
                string query = $@"
                        INSERT INTO
                            {DBTables.PATIENT_CHART}
                        VALUES (
                            null,
                            @createdAt,
                            @patientId,
                            @status
                        )";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@createdAt", createdAt);
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
                cmd.Prepare();

                await cmd.ExecuteNonQueryAsync();

                connection.Close();
                return cmd.LastInsertedId;
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }

        /* Read one patient chart by patient id */

        public async Task<PatientChart> readPatientChart(int patientId) {
            try {
                string query = $@"
                        SELECT *
                        FROM
                            {DBTables.PATIENT_CHART}
                        WHERE
                            {DBTables.PATIENT_CHART}.patient = @patientId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                PatientChart patientChart = new PatientChart();

                while (reader.Read()) {
                    Patient currentPatient = new Patient(
                        reader.GetInt32(reader.GetOrdinal("patient")),
                        "",
                        "",
                        "",
                        "",
                        DateTime.Now,
                        ""
                    );

                    PatientChart chart = new PatientChart(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetDateTime(reader.GetOrdinal("date_created")),
                        currentPatient
                    );

                    return chart;
                }

                return null;
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }
    }
}

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
    public class Allergen {
        public int Id { get; set; }
        public PatientChart PatientChart { get; set; }
        public Medicament Medicament { get; set; }

        public Allergen() {
            this.Id = 0;
            this.PatientChart = null;
            this.Medicament = null;
        }

        public Allergen(
            int id,
            PatientChart patientChart,
            Medicament medicament
        ) {
            this.Id = id;
            this.PatientChart = patientChart;
            this.Medicament = medicament;
        }

        /* Read all allergens */

        public async Task<List<Allergen>> readAllergens(int chartId) {
            try {
                string query = $@"
                        SELECT
                            allergen.id as allergenId,
                            allergen.patient_chart as allergenPatientChartId,
                            medicament.id as medicamentId,
                            medicament.name as medicamentName
                        FROM 
                            {DBTables.ALLERGEN} as allergen
                            INNER JOIN
                                {DBTables.MEDICAMENT} as medicament
                                ON
                                {DBTables.ALLERGEN}.medicament = {DBTables.MEDICAMENT}.id
                        WHERE
                            allergen.patient_chart = @chartId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@chartId", chartId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Allergen> allergens = new List<Allergen>();

                while (reader.Read()) {
                    PatientChart chart = new PatientChart(
                        reader.GetInt32(reader.GetOrdinal("allergenPatientChartId")),
                        DateTime.Now,
                        null
                    );

                    Medicament medicament = new Medicament(
                        reader.GetInt32(reader.GetOrdinal("medicamentId")),
                        reader.GetString(reader.GetOrdinal("medicamentName")),
                        "",
                        DateTime.Now,
                        ""
                    );

                    Allergen allergen = new Allergen(
                        reader.GetInt32(reader.GetOrdinal("allergenId")),
                        chart,
                        medicament
                    );

                    allergens.Add(allergen);
                }

                return allergens;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Read all allergens for one patient */

        public async Task<List<Allergen>> readAllergensForPatient(int patientId) {
            try {
                string query = $@"
                        SELECT
                            allergen.id as allergenId,
                            allergen.patient_chart as allergenPatientChartId,
                            medicament.id as medicamentId,
                            medicament.name as medicamentName
                        FROM 
                            {DBTables.ALLERGEN} as allergen
                            INNER JOIN
                                {DBTables.MEDICAMENT} as medicament
                                ON
                                {DBTables.ALLERGEN}.medicament = {DBTables.MEDICAMENT}.id
                            INNER JOIN
                                {DBTables.PATIENT_CHART}
                                ON
                                {DBTables.PATIENT_CHART}.id = {DBTables.ALLERGEN}.patient_chart
                        WHERE
                            {DBTables.PATIENT_CHART}.patient = @patientId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Allergen> allergens = new List<Allergen>();

                while (reader.Read()) {
                    PatientChart chart = new PatientChart(
                        reader.GetInt32(reader.GetOrdinal("allergenPatientChartId")),
                        DateTime.Now,
                        null
                    );

                    Medicament medicament = new Medicament(
                        reader.GetInt32(reader.GetOrdinal("medicamentId")),
                        reader.GetString(reader.GetOrdinal("medicamentName")),
                        "",
                        DateTime.Now,
                        ""
                    );

                    Allergen allergen = new Allergen(
                        reader.GetInt32(reader.GetOrdinal("allergenId")),
                        chart,
                        medicament
                    );

                    allergens.Add(allergen);
                }

                return allergens;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create allergen */

        public async Task<long> createAllergen(
            int patientChart,
            int medicament
        ) {
            try {
                string query = $@"
                        INSERT INTO
                            {DBTables.ALLERGEN}
                        VALUES (
                            null,
                            @patientChartId,
                            @medicamentId,
                            @status
                        )";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@patientChartId", patientChart);
                cmd.Parameters.AddWithValue("@medicamentId", medicament);
                cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
                cmd.Prepare();

                await cmd.ExecuteNonQueryAsync();

                connection.Close();
                return cmd.LastInsertedId;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

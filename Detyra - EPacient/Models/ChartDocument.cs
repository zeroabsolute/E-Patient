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
    public class ChartDocument {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
        public DateTime DateCreated { get; set; }
        public PatientChart PatientChart { get; set; }

        public ChartDocument() {
            this.Id = 0;
            this.Name = "";
            this.Type = "";
            this.URL = "";
            this.DateCreated = DateTime.Now;
            this.PatientChart = null;
        }

        public ChartDocument(
            int id,
            string name,
            string type,
            string url,
            DateTime dateCreated,
            PatientChart patientChart
        ) {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.Name = name;
            this.Type = type;
            this.URL = url;
            this.PatientChart = patientChart;
        }

        /* Read all chart documents */

        public async Task<List<ChartDocument>> readChartDocuments(int chartId) {
            try {
                string query = $@"
                        SELECT *
                        FROM 
                            {DBTables.CHART_DOCUMENT}
                        WHERE
                            patient_chart = @chartId";


                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@chartId", chartId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<ChartDocument> docs = new List<ChartDocument>();

                while (reader.Read()) {
                    PatientChart chart = new PatientChart(
                        reader.GetInt32(reader.GetOrdinal("patient_chart")),
                        DateTime.Now,
                        null
                    );

                    ChartDocument doc = new ChartDocument(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetString(reader.GetOrdinal("type")),
                        reader.GetString(reader.GetOrdinal("url")),
                        reader.GetDateTime(reader.GetOrdinal("date_created")),
                        chart
                    );

                    docs.Add(doc);
                }

                return docs;
            } catch (Exception e) {
                throw e;
            }
        }     
        /* Create chart document */

        public async Task<long> createChartDocument(
            string name,
            string type,
            string url,
            string dateCreated,
            int patientChart
        ) {
            try {
                string query = $@"
                        INSERT INTO
                            {DBTables.CHART_DOCUMENT}
                        VALUES (
                            null,
                            @name,
                            @type,
                            @url,
                            @dateCreated,
                            @patientChartId,
                            @status
                        )";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@dateCreated", dateCreated);
                cmd.Parameters.AddWithValue("@patientChartId", patientChart);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Collections;
using System.Security.Cryptography;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Models {
    public class ChartDocument {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
        public byte[] File { get; set; }
        public string Hash { get; set; }
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
            PatientChart patientChart,
            byte[] file,
            string hash
        ) {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.Name = name;
            this.Type = type;
            this.URL = url;
            this.PatientChart = patientChart;
            this.File = file;
            this.Hash = hash;
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

                    int size = reader.GetInt32(reader.GetOrdinal("file_size"));
                    byte[] file = new byte[size];
                    reader.GetBytes(reader.GetOrdinal("file"), 0, file, 0, size);

                    ChartDocument doc = new ChartDocument(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetString(reader.GetOrdinal("type")),
                        reader.GetString(reader.GetOrdinal("url")),
                        reader.GetDateTime(reader.GetOrdinal("date_created")),
                        chart,
                        file,
                        reader.GetString(reader.GetOrdinal("hash"))
                    );

                    docs.Add(doc);
                }

                return docs;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create chart document */
        private string getFileHash(byte[] file) {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            return Convert.ToBase64String(sha1.ComputeHash(file));
        }

        public async Task<long> createChartDocument(
            string name,
            string type,
            string url,
            string dateCreated,
            int patientChart,
            byte[] file,
            int fileSize
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
                            @status,
                            @file,
                            @fileSize,
                            @hash
                        )";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                // Read existing docs
                List<ChartDocument> existingDocs = await this.readChartDocuments(patientChart);

                existingDocs.ForEach((item) => {
                    byte[] fileData = item.File;
                    string fileHash = item.Hash;
                    string itemHash = this.getFileHash(file);

                    bool fileExists = file.Length == fileData.Length && fileHash.Equals(itemHash);

                    if (fileExists) {
                        throw new Exception("Skedari është shtuar më parë");
                    }
                });

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@dateCreated", dateCreated);
                cmd.Parameters.AddWithValue("@patientChartId", patientChart);
                cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
                cmd.Parameters.AddWithValue("@file", file);
                cmd.Parameters.AddWithValue("@fileSize", fileSize);
                cmd.Parameters.AddWithValue("@hash", this.getFileHash(file));
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

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
    class Service {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fee { get; set; }
        public string Description { get; set; }

        public Service() {
            this.Id = -1;
            this.Name = "";
            this.Fee = 0;
            this.Description = "";
        }

        public Service(
            int id,
            string name,
            int fee,
            string description
        ) {
            this.Id = id;
            this.Name = name;
            this.Fee = fee;
            this.Description = description;
        }

        /**
         * Method to read services from the database
         */

        public async Task<List<Service>> readServices() {
            try {
                string query = $@"
                        SELECT *
                        FROM {DBTables.SERVICE}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Service> services = new List<Service>();

                while (reader.Read()) {
                    Service currentService = new Service(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetInt32(reader.GetOrdinal("fee")),
                        reader.GetString(reader.GetOrdinal("description"))
                    );

                    services.Add(currentService);
                }

                return services;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create service */

        public async Task<long> createService(
            string name,
            int fee,
            string description
        ) {
            try {
                bool nameIsValid = name != null && name.Length > 0;
                bool feeIsValid = fee != 0;
                bool descriptionIsValid = description != null && description.Length > 0;

                if (nameIsValid && feeIsValid && descriptionIsValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.SERVICE}
                        VALUES (
                            null,
                            @name,
                            @fee,
                            @description,
                            @status
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fee", fee);
                    cmd.Parameters.AddWithValue("@description", description);
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

        /* Update service */

        public async Task<long> updateService(
            long id,
            string name,
            int fee,
            string description
        ) {
            try {
                bool idIsValid = id != -1;
                bool nameIsValid = name != null && name.Length > 0;
                bool feeIsValid = fee != 0;
                bool descriptionIsValid = description != null && description.Length > 0;

                if (idIsValid && nameIsValid && feeIsValid && descriptionIsValid) {
                    string query = $@"
                        UPDATE
                            {DBTables.SERVICE}
                        SET
                            name = @name,
                            fee = @fee,
                            description = @description
                        WHERE
                            {DBTables.SERVICE}.id = @id";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fee", fee);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@id", id);
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
         * Get total number of services
         */

        public async Task<int> getServicesCount() {
            try {
                string query = $@"
                    SELECT
                        COUNT(*)
                    FROM 
                        {DBTables.SERVICE}";

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

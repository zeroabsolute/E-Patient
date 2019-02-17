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
    public class Medicament {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime  ExpirationDate { get; set; }
        public string Ingredients { get; set; }     
        

        public Medicament() {
            this.Id = -1;
            this.Name = "";
            this.Description = "";
            this.ExpirationDate = new DateTime();
            this.Ingredients = "";
        }

        public Medicament(
            int id,
            string name,
            string description,
            DateTime expirationDate,
            string ingredients
        ) {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ExpirationDate = expirationDate;
            this.Ingredients = ingredients;
        }

        /**
         * Method to read medicaments from the database
         */

        public async Task<List<Medicament>> readMedicaments() {
            try {
                string query = $@"
                        SELECT *
                        FROM {DBTables.MEDICAMENT}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Medicament> medicaments = new List<Medicament>();

                while (reader.Read()) {
                    Medicament currentMedicament = new Medicament(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetString(reader.GetOrdinal("description")),
                        reader.GetDateTime(reader.GetOrdinal("expiration_date")),
                        reader.GetString(reader.GetOrdinal("ingredients"))
                    );

                    medicaments.Add(currentMedicament);
                }

                return medicaments;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create medicament */

        public async Task<long> createMedicament(
            string name,
            string description,
            string expirationDate,
            string ingredients
        ) {
            try {
                bool nameIsValid = name != null && name.Length > 0;
                bool descriptionIsValid = description != null && description.Length > 0;
                bool expirationDateIsValid = expirationDate != null && expirationDate.Length > 0;
                bool ingredientsIsValid = ingredients != null && ingredients.Length > 0;

                if (nameIsValid && expirationDateIsValid && descriptionIsValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.MEDICAMENT}
                        VALUES (
                            null,
                            @name,
                            @description,
                            @expirationDate,
                            @ingredients,
                            @status
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
                    cmd.Parameters.AddWithValue("@ingredients", ingredients);
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

        /* Update medicament */

        public async Task<long> updateMedicament(
            long id,
            string name,
            string description,
            string expirationDate,
            string ingredients
        ) {
            try {
                bool idIsValid = id != -1;
                bool nameIsValid = name != null && name.Length > 0;
                bool descriptionIsValid = description != null && description.Length > 0;
                bool expirationDateIsValid = expirationDate != null && expirationDate.Length > 0;
                bool ingredientsIsValid = ingredients != null && ingredients.Length > 0;

                if (idIsValid && nameIsValid && descriptionIsValid && expirationDateIsValid && ingredientsIsValid) {
                    string query = $@"
                        UPDATE
                            {DBTables.MEDICAMENT}
                        SET
                            name = @name,
                            description = @description,
                            expiration_date = @expirationDate,
                            ingredients = @ingredients
                        WHERE
                            {DBTables.MEDICAMENT}.id = @id";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
                    cmd.Parameters.AddWithValue("@ingredients", ingredients);
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
    }
}

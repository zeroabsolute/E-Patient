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
    public class Prescription {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Reservation { get; set; }
        

        public Prescription() {
            this.Id = -1;
            this.Description = "";
            this.Reservation = -1;
        }

        public Prescription(
            int id,
            string description,
            int reservation
        ) {
            this.Id = id;
            this.Description = description;
            this.Reservation = reservation;
        }

        /**
         * Method to read prescription for a given reservation
         */

        public async Task<Prescription> readPrescriptionForReservation(int reservationId) {
            try {
                string query = $@"
                        SELECT *
                        FROM 
                            {DBTables.PRESCRIPTION}
                        WHERE
                            {DBTables.PRESCRIPTION}.reservation = @reservationId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@reservationId", reservationId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Medicament> medicaments = new List<Medicament>();

                while (reader.Read()) {
                    return new Prescription(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetString(reader.GetOrdinal("description")),
                        reader.GetInt32(reader.GetOrdinal("reservation"))
                    );
                }

                return null;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create prescription */

        public async Task<long> createPrescription(
            string description,
            int reservationId
        ) {
            try {
                bool descriptionIsValid = description != null && description.Length > 0;
                bool reservationIdIsValid = reservationId > 0;

                if (reservationIdIsValid && descriptionIsValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.PRESCRIPTION}
                        VALUES (
                            null,
                            @description,
                            @reservation,
                            @status
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@reservation", reservationId);
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
    }
}

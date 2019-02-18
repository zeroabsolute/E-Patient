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
    public class PrescriptionMedicament {
        public int Id { get; set; }
        public int Prescription { get; set; }
        public int Medicament { get; set; }
        

        public PrescriptionMedicament() {
            this.Id = -1;
            this.Prescription = -1;
            this.Medicament = -1;
        }

        public PrescriptionMedicament(
            int id,
            int prescription,
            int medicament
        ) {
            this.Id = id;
            this.Prescription = prescription;
            this.Medicament = medicament;
        }

        /**
         * Method to read medicaments for a given prescription
         */

        public async Task<List<PrescriptionMedicament>> readMedicamentsForPrescription(int prescriptionId) {
            try {
                string query = $@"
                        SELECT *
                        FROM 
                            {DBTables.PRESCRIPTION_MEDICAMENT}
                        WHERE
                            {DBTables.PRESCRIPTION_MEDICAMENT}.prescription = @prescriptionId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@prescriptionId", prescriptionId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<PrescriptionMedicament> medicaments = new List<PrescriptionMedicament>();

                while (reader.Read()) {
                    PrescriptionMedicament currentMedicament = new PrescriptionMedicament(
                        reader.GetInt32(reader.GetOrdinal("id")),
                        reader.GetInt32(reader.GetOrdinal("prescription")),
                        reader.GetInt32(reader.GetOrdinal("medicament"))
                    );

                    medicaments.Add(currentMedicament);
                }

                return medicaments;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create prescription medicaments */

        public async Task<long> createPrescriptionMedicaments(
            long prescriptionId,
            List<Medicament> medicaments
        ) {
            try {
                bool prescriptionIdIsValid = prescriptionId > 0;
                bool medicamentsAreValid = medicaments.Count > 0;

                if (prescriptionIdIsValid && medicamentsAreValid) {
                    string query = $@"
                        INSERT INTO
                            {DBTables.PRESCRIPTION_MEDICAMENT}
                        VALUES";

                    int index = 0;
                    medicaments.ForEach((item) => {
                        query = $"{query} (null, {prescriptionId}, {item.Id}, {Statuses.ACTIVE.Id})";

                        if (index != medicaments.Count - 1) {
                            query = $"{query},";
                        }

                        index += 1;
                    });

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
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

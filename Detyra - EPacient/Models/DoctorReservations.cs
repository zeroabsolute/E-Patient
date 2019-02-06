using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;

namespace Detyra___EPacient.Models {
    class DoctorReservations {
        public DateTime Date { get; set; }
        public int ReservationsCount { get; set; }

        public DoctorReservations() {
            this.Date = DateTime.Now;
            this.ReservationsCount = 0;
        }

        public DoctorReservations(
            DateTime date,
            int reservationsCount
        ) {
            this.Date = date;
            this.ReservationsCount = reservationsCount;
        }

        /**
         * Method to get reservations for each doctor from the database
         */
        
        public async Task<List<DoctorReservations>> readDoctorReservations() {
            try {
                string query = $@"
                        ";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<DoctorReservations> data = new List<DoctorReservations>();

                while (reader.Read()) {
                }

                return data;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detyra___EPacient.Models {
    class ReservationAnalytics {
        public int Day { get; set; }
        public int ReservationsCount { get; set; }

        public ReservationAnalytics() {
            this.Day = 1;
            this.ReservationsCount = 0;
        }

        public ReservationAnalytics(int day, int count) {
            this.Day = day;
            this.ReservationsCount = count;
        }

        /**
         * Read reservations for a given doctor and a given month
         */

        public async Task<List<ReservationAnalytics>> readReservationAnalytics(
            int doctorId,
            string month,
            string year
        ) {
            try {
                bool doctorIdIsValid = doctorId > 0;
                bool monthIsValid = month != null && month.Length > 0;
                bool yearIsValid = month != null && month.Length > 0;

                if (doctorIdIsValid && monthIsValid && yearIsValid) {
                    string query = $@"
                        SELECT *
                        FROM
                            {DBTables.RESERVATION}
                        WHERE
                            doctor = @doctorId
                            AND
                            MONTH(start_datetime) = @month
                            AND
                            YEAR(start_datetime) = @year";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@doctorId", doctorId);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Prepare();

                    DbDataReader reader = await cmd.ExecuteReaderAsync();
                    List<ReservationAnalytics> data = new List<ReservationAnalytics>();

                    for (int i = 1; i <= 31; i += 1) {
                        data.Add(new ReservationAnalytics(i, 0));
                    }

                    while (reader.Read()) {
                        DateTime start = reader.GetDateTime(reader.GetOrdinal("start_datetime"));
                        DateTime end = reader.GetDateTime(reader.GetOrdinal("end_datetime"));
                        string dayString = start.Day.ToString();
                        int day = Int32.Parse(dayString);

                        ReservationAnalytics found = data.Find(item => item.Day == day);
                        if (found != null) {
                            found.ReservationsCount += 1;
                        } else {
                            data.Add(new ReservationAnalytics(day, 1));
                        }
                    }

                    return data;
                } else {
                    throw new Exception("Input i gabuar ose i pamjaftueshëm");
                }
            } catch (Exception e) {
                throw e;
            }
        } 
    }
}

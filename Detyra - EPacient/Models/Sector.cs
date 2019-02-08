using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;

namespace Detyra___EPacient.Models {
    class Sector {
        public int Id { get; set; }
        public string Name { get; set; }

        public Sector() {
            this.Id = -1;
            this.Name = "";
        }

        public Sector(int id, string name) {
            this.Id = id;
            this.Name = name;
        }

        /**
         * Method to read sectors from the database
         */
        
        public async Task<List<Sector>> readSectors() {
            try {
                string query = $@"
                        SELECT *
                        FROM {DBTables.SECTOR}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Sector> sectors = new List<Sector>();

                while (reader.Read()) {
                    int sectorId = reader.GetInt32(reader.GetOrdinal("id"));
                    string sectorName = reader.GetString(reader.GetOrdinal("name"));
                    Sector currentSector = new Sector(sectorId, sectorName);

                    sectors.Add(currentSector);
                }

                return sectors;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

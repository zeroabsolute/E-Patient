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
    class Role {
        public int Id { get; set; }
        public string Name { get; set; }

        public Role() {
            this.Id = -1;
            this.Name = "";
        }

        public Role(int id, string name) {
            this.Id = id;
            this.Name = name;
        }

        /**
         * Method to read roles from the database
         */
        
        public async Task<List<Role>> readRoles() {
            try {
                string query = $@"
                        SELECT *
                        FROM {DBTables.ROLE}";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Role> roles = new List<Role>();

                while (reader.Read()) {
                    int roleId = reader.GetInt32(reader.GetOrdinal("id"));
                    string roleName = reader.GetString(reader.GetOrdinal("name"));
                    Role currentRole = new Role(roleId, roleName);

                    roles.Add(currentRole);
                }

                return roles;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

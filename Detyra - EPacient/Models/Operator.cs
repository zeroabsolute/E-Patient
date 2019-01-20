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
    class Operator {
        public User User { get; set; }
        public int OperatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Operator() {
            this.User = null;
            this.OperatorId = -1;
            this.FirstName = "";
            this.LastName = "";
            this.DateOfBirth = new DateTime();
        }

        public Operator(
            User user,
            int operatorId,
            string firstName,
            string lastName,
            DateTime dob
        ) {
            this.User = User;
            this.OperatorId = operatorId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dob;
        }

        /**
         * Method to read operators from the database
         */
        
        public async Task<List<Operator>> readOperators() {
            try {
                string query = $@"
                        SELECT
                            role.id as roleId,
                            role.name as roleName,
                            user.id as userId,
                            user.email as userEmail,
                            operator.id as operatorId,
                            operator.first_name as firstName,
                            operator.last_name as lastName,
                            operator.date_of_birth as dateOfBirth
                        FROM 
                            {DBTables.OPERATOR} as operator
                            INNER JOIN
                                {DBTables.USER} as user
                                ON
                                {DBTables.OPERATOR}.user = {DBTables.USER}.id
                            INNER JOIN
                                {DBTables.ROLE} as role
                                ON
                                {DBTables.USER}.role = {DBTables.ROLE}.id";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Operator> operators = new List<Operator>();

                while (reader.Read()) {
                    int roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                    string roleName = reader.GetString(reader.GetOrdinal("roleName"));
                    int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                    string userEmail = reader.GetString(reader.GetOrdinal("userEmail"));
                    int operatorId = reader.GetInt32(reader.GetOrdinal("operatorId"));
                    string firstName = reader.GetString(reader.GetOrdinal("firstName"));
                    string lastName = reader.GetString(reader.GetOrdinal("lastName"));
                    DateTime dob = reader.GetDateTime(reader.GetOrdinal("dateOfBirth"));

                    Role currentRole = new Role(roleId, roleName);
                    User currentUser = new User(userId, currentRole, userEmail, null);
                    Operator currentOperator = new Operator(
                        currentUser,
                        operatorId,
                        firstName,
                        lastName,
                        dob
                    );

                    operators.Add(currentOperator);
                }

                return operators;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

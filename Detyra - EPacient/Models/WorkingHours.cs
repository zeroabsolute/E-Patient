using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

using Detyra___EPacient.Config;
using Detyra___EPacient.Constants;
using MySql.Data.MySqlClient;

namespace Detyra___EPacient.Models {
    class WorkingHours {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime MondayStartTime { get; set; }
        public DateTime MondayEndTime { get; set; }
        public DateTime TuesdayStartTime { get; set; }
        public DateTime TuesdayEndTime { get; set; }
        public DateTime WednesdayStartTime { get; set; }
        public DateTime WednesdayEndTime { get; set; }
        public DateTime ThursdayStartTime { get; set; }
        public DateTime ThursdayEndTime { get; set; }
        public DateTime FridayStartTime { get; set; }
        public DateTime FridayEndTime { get; set; }
        public DateTime SaturdayStartTime { get; set; }
        public DateTime SaturdayEndTime { get; set; }
        public DateTime SundayStartTime { get; set; }
        public DateTime SundayEndTime { get; set; }

        public WorkingHours() {
            this.Id = 0;
            this.Employee = null;
        }

        public WorkingHours(
            int id,
            Employee employee,
            DateTime mondayStart,
            DateTime mondayEnd,
            DateTime tuesdayStart,
            DateTime tuesdayEnd,
            DateTime wednesdayStart,
            DateTime wednesdayEnd,
            DateTime thursdayStart,
            DateTime thursdayEnd,
            DateTime fridayStart,
            DateTime fridayEnd,
            DateTime saturdayStart,
            DateTime saturdayEnd,
            DateTime sundayStart,
            DateTime sundayEnd
        ) {
            this.Id = id;
            this.Employee = employee;
            this.MondayStartTime = mondayStart;
            this.MondayEndTime = mondayEnd;
            this.TuesdayStartTime = tuesdayStart;
            this.TuesdayEndTime = tuesdayEnd;
            this.WednesdayStartTime = wednesdayStart;
            this.WednesdayEndTime = wednesdayEnd;
            this.ThursdayStartTime = thursdayStart;
            this.ThursdayEndTime = thursdayEnd;
            this.FridayStartTime = fridayStart;
            this.FridayEndTime = fridayEnd;
            this.SaturdayStartTime = saturdayStart;
            this.SaturdayEndTime = saturdayEnd;
            this.SundayStartTime = sundayStart;
            this.SundayEndTime = sundayEnd;
        }

        /**
         * Method to read one entry from the database
         */

        public async Task<WorkingHours> readWorkingHours(int employeeId) {
            try {
                string query = $@"
                        SELECT
                            role.id as roleId,
                            role.name as roleName,
                            user.id as userId,
                            user.email as userEmail,
                            user.status as userStatus,
                            employee.id as employeeId,
                            employee.first_name as employeeFirstName,
                            employee.last_name as employeeLastName,
                            employee.phone_number as employeePhoneNumber,
                            employee.address as employeeAddress,
                            employee.date_of_birth as employeeDateOfBirth,
                            workingHours.id as workingHoursId,
                            workingHours.monday_start_time as workingHoursMondayStart,
                            workingHours.monday_end_time as workingHoursMondayEnd,
                            workingHours.tuesday_start_time as workingHoursTuesdayStart,
                            workingHours.tuesday_end_time as workingHoursTuesdayEnd,
                            workingHours.wednesday_start_time as workingHoursWednesdayStart,
                            workingHours.wednesday_end_time as workingHoursWednesdayEnd,
                            workingHours.thursday_start_time as workingHoursThursdayStart,
                            workingHours.thursday_end_time as workingHoursThursdayEnd,
                            workingHours.friday_start_time as workingHoursFridayStart,
                            workingHours.friday_end_time as workingHoursFridayEnd,
                            workingHours.saturday_start_time as workingHoursSaturdayStart,
                            workingHours.saturday_end_time as workingHoursSaturdayEnd,
                            workingHours.sunday_start_time as workingHoursSundayStart,
                            workingHours.sunday_end_time as workingHoursSundayEnd
                        FROM 
                            {DBTables.WORKING_HOURS} as workingHours
                            INNER JOIN
                                {DBTables.EMPLOYEE} as employee
                                ON
                                employee = {DBTables.EMPLOYEE}.id
                            INNER JOIN
                                {DBTables.USER} as user
                                ON
                                {DBTables.EMPLOYEE}.user = {DBTables.USER}.id
                            INNER JOIN
                                {DBTables.ROLE} as role
                                ON
                                {DBTables.USER}.role = {DBTables.ROLE}.id
                        WHERE
                            employee = @employeeId";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read()) {
                    int roleId = reader.GetInt32(reader.GetOrdinal("roleId"));
                    string roleName = reader.GetString(reader.GetOrdinal("roleName"));
                    int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                    int userStatus = reader.GetInt32(reader.GetOrdinal("userStatus"));
                    string userEmail = reader.GetString(reader.GetOrdinal("userEmail"));
                    int employee = reader.GetInt32(reader.GetOrdinal("employeeId"));
                    string employeeFirstName = reader.GetString(reader.GetOrdinal("employeeFirstName"));
                    string employeeLastName = reader.GetString(reader.GetOrdinal("employeeLastName"));
                    string employeePhoneNumber = reader.GetString(reader.GetOrdinal("employeePhoneNumber"));
                    string employeeAddress = reader.GetString(reader.GetOrdinal("employeeAddress"));
                    DateTime employeeDOB = reader.GetDateTime(reader.GetOrdinal("employeeDateOfBirth"));

                    Role currentRole = new Role(roleId, roleName);
                    User currentUser = new User(userId, currentRole, userStatus, userEmail, null);
                    Employee currentEmployee = new Employee(
                        employeeId,
                        employeeFirstName,
                        employeeLastName,
                        employeePhoneNumber,
                        employeeAddress,
                        employeeDOB,
                        currentUser
                    );
                    WorkingHours workingHours = new WorkingHours(
                        reader.GetInt32(reader.GetOrdinal("workingHoursId")),
                        currentEmployee,
                        reader.GetDateTime(reader.GetOrdinal("workingHoursMondayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursMondayEnd")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursTuesdayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursTuesdayEnd")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursWednesdayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursWednesdayEnd")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursThursdayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursThursdayEnd")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursFridayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursFridayEnd")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursSaturdayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursSaturdayEnd")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursSundayStart")),
                        reader.GetDateTime(reader.GetOrdinal("workingHoursSundayEnd"))
                    );

                    return workingHours;
                }

                return null;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create or update working hours */

        public async Task<long> updateWorkingHours(
            long id,
            int employeeId,
            DateTime mondayStart,
            DateTime mondayEnd,
            DateTime tuesdayStart,
            DateTime tuesdayEnd,
            DateTime wednesdayStart,
            DateTime wednesdayEnd,
            DateTime thursdayStart,
            DateTime thursdayEnd,
            DateTime fridayStart,
            DateTime fridayEnd,
            DateTime saturdayStart,
            DateTime saturdayEnd,
            DateTime sundayStart,
            DateTime sundayEnd
        ) {
            try {
                string query = "";
                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                if (employeeId < 1) {
                    throw new Exception("Asnjë punonjës nuk është zgjedhur");
                }

                if (id == -1) {
                    query = $@"
                        INSERT INTO
                            {DBTables.WORKING_HOURS}
                        VALUES (
                            null,
                            @mondayStart,
                            @mondayEnd,
                            @tuesdayStart,
                            @tuesdayEnd,
                            @wednesdayStart,
                            @wednesdayEnd,
                            @thursdayStart,
                            @thursdayEnd,
                            @fridayStart,
                            @fridayEnd,
                            @saturdayStart,
                            @saturdayEnd,
                            @sundayStart,
                            @sundayEnd,
                            @employee,
                            @status
                        )";
                } else {
                    query = $@"
                        UPDATE
                            {DBTables.WORKING_HOURS}
                        SET
                            monday_start_time = @mondayStart,
                            monday_end_time = @mondayEnd,
                            tuesday_start_time = @tuesdayStart,
                            tuesday_end_time = @tuesdayEnd,
                            wednesday_start_time = @wednesdayStart,
                            wednesday_end_time = @wednesdayEnd,
                            thursday_start_time = @thursdayStart,
                            thursday_end_time = @thursdayEnd,
                            friday_start_time = @fridayStart,
                            friday_end_time = @fridayEnd,
                            saturday_start_time = @saturdayStart,
                            saturday_end_time = @saturdayEnd,
                            sunday_start_time = @sundayStart,
                            sunday_end_time = @sundayEnd
                        WHERE
                            {DBTables.WORKING_HOURS}.id = @workingHoursId";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);

                if (id == -1) {
                    cmd.Parameters.AddWithValue("@employee", employeeId);
                    cmd.Parameters.AddWithValue("@status", Statuses.ACTIVE.Id);
                } else {
                    cmd.Parameters.AddWithValue("@workingHoursId", id);
                }

                cmd.Parameters.AddWithValue("@mondayStart", mondayStart);
                cmd.Parameters.AddWithValue("@mondayEnd", mondayEnd);
                cmd.Parameters.AddWithValue("@tuesdayStart", tuesdayStart);
                cmd.Parameters.AddWithValue("@tuesdayEnd", tuesdayEnd);
                cmd.Parameters.AddWithValue("@wednesdayStart", wednesdayStart);
                cmd.Parameters.AddWithValue("@wednesdayEnd", wednesdayEnd);
                cmd.Parameters.AddWithValue("@thursdayStart", thursdayStart);
                cmd.Parameters.AddWithValue("@thursdayEnd", thursdayEnd);
                cmd.Parameters.AddWithValue("@fridayStart", fridayStart);
                cmd.Parameters.AddWithValue("@fridayEnd", fridayEnd);
                cmd.Parameters.AddWithValue("@saturdayStart", saturdayStart);
                cmd.Parameters.AddWithValue("@saturdayEnd", saturdayEnd);
                cmd.Parameters.AddWithValue("@sundayStart", sundayStart);
                cmd.Parameters.AddWithValue("@sundayEnd", sundayEnd);

                cmd.Prepare();

                await cmd.ExecuteNonQueryAsync();

                connection.Close();
                return cmd.LastInsertedId;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

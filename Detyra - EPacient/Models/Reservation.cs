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
    class Reservation {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Service Service { get; set; }
        public Operator CreatedBy { get; set; }
        public Patient Patient { get; set; }
        public Nurse Nurse { get; set; }
        public Doctor Doctor { get; set; }

        public Reservation() {
            this.Id = -1;
            this.StartDateTime = DateTime.Now;
            this.EndDateTime = DateTime.Now;
            this.Service = null;
            this.CreatedBy = null;
            this.Patient = null;
            this.Nurse = null;
            this.Doctor = null;
        }

        public Reservation(
            int id,
            DateTime start,
            DateTime end,
            Service service,
            Operator createdBy,
            Patient patient,
            Nurse nurse,
            Doctor doctor
        ) {
            this.Id = id;
            this.StartDateTime = start;
            this.EndDateTime = end;
            this.Service = service;
            this.CreatedBy = createdBy;
            this.Patient = patient;
            this.Nurse = nurse;
            this.Doctor = doctor;
        }

        /**
         * Method to read reservations from the database
         */

        public async Task<List<Reservation>> readReservations() {
            try {
                string query = $@"
                        SELECT
                            reservation.id as reservationId,
                            reservation.start_datetime as reservationStart,
                            reservation.end_datetime as reservationEnd,
                            service.id as serviceId,
                            service.name as serviceName,
                            service.fee as serviceFee,
                            service.description as serviceDescription,
                            operator.id as operatorId,
                            operator.first_name as operatorFirstName,
                            operator.last_name as operatorLastName,
                            patient.id as patientId,
                            patient.first_name as patientFirstName,
                            patient.last_name as patientLastName,
                            nurse.id as nurseId,
                            nurseEmployee.first_name as nurseFirstName,
                            nurseEmployee.last_name as nurseLastName,
                            doctor.id as doctorId,
                            doctorEmployee.first_name as doctorFirstName,
                            doctorEmployee.last_name as doctorLastName
                        FROM 
                            {DBTables.RESERVATION} as reservation
                            INNER JOIN
                            {DBTables.SERVICE} as service
                            ON
                            {DBTables.RESERVATION}.service = {DBTables.SERVICE}.id
                            INNER JOIN
                            {DBTables.OPERATOR} as operator
                            ON
                            {DBTables.RESERVATION}.created_by = {DBTables.OPERATOR}.id
                            INNER JOIN
                            {DBTables.PATIENT} as patient
                            ON
                            {DBTables.RESERVATION}.patient = {DBTables.PATIENT}.id
                            INNER JOIN (
                                {DBTables.NURSE} as nurse
                                INNER JOIN
                                {DBTables.EMPLOYEE} as nurseEmployee
                                ON
                                {DBTables.NURSE}.employee = nurseEmployee.id
                            )
                            ON
                            {DBTables.RESERVATION}.nurse = {DBTables.NURSE}.id
                            INNER JOIN (
                                {DBTables.DOCTOR} as doctor
                                INNER JOIN
                                {DBTables.EMPLOYEE} as doctorEmployee
                                ON
                                {DBTables.DOCTOR}.employee = doctorEmployee.id
                            )
                            ON
                            {DBTables.RESERVATION}.doctor = {DBTables.DOCTOR}.id";

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                List<Reservation> reservations = new List<Reservation>();

                while (reader.Read()) {
                    Service currentService = new Service(
                        reader.GetInt32(reader.GetOrdinal("serviceId")),
                        reader.GetString(reader.GetOrdinal("serviceName")),
                        reader.GetInt32(reader.GetOrdinal("serviceFee")),
                        reader.GetString(reader.GetOrdinal("serviceDescription"))
                    );

                    Operator currentOperator = new Operator(
                        null,
                        reader.GetInt32(reader.GetOrdinal("operatorId")),
                        reader.GetString(reader.GetOrdinal("operatorFirstName")),
                        reader.GetString(reader.GetOrdinal("operatorLastName")),
                        DateTime.Now
                    );

                    Patient currentPatient = new Patient(
                        reader.GetInt32(reader.GetOrdinal("patientId")),
                        reader.GetString(reader.GetOrdinal("patientFirstName")),
                        reader.GetString(reader.GetOrdinal("patientLastName")),
                        "",
                        "",
                        DateTime.Now,
                        ""
                    );

                    Employee nurseEmployee = new Employee(
                        -1,
                        reader.GetString(reader.GetOrdinal("nurseFirstName")),
                        reader.GetString(reader.GetOrdinal("nurseLastName")),
                        "",
                        "",
                        DateTime.Now,
                        null
                    );
                    Nurse currentNurse = new Nurse(
                        reader.GetInt32(reader.GetOrdinal("nurseId")),
                        nurseEmployee
                    );

                    Employee doctorEmployee = new Employee(
                        -1,
                        reader.GetString(reader.GetOrdinal("nurseFirstName")),
                        reader.GetString(reader.GetOrdinal("nurseLastName")),
                        "",
                        "",
                        DateTime.Now,
                        null
                    );
                    Doctor currentDoctor = new Doctor(
                        reader.GetInt32(reader.GetOrdinal("nurseId")),
                        nurseEmployee,
                        ""
                    );

                    Reservation currentReservation = new Reservation(
                        reader.GetInt32(reader.GetOrdinal("reservationId")),
                        reader.GetDateTime(reader.GetOrdinal("reservationStart")),
                        reader.GetDateTime(reader.GetOrdinal("reservationEnd")),
                        currentService,
                        currentOperator,
                        currentPatient,
                        currentNurse,
                        currentDoctor
                    );

                    reservations.Add(currentReservation);
                }

                return reservations;
            } catch (Exception e) {
                throw e;
            }
        }

        /* Create reservation */

        private async Task<bool> checkIfEmployeeIsWorking(
            int employeeId, 
            string start, 
            string end
        ) {
            try {
                WorkingHours employeeWorkingHours = await new WorkingHours().readWorkingHours(employeeId);
                DateTime startDateTime = DateTime.ParseExact(start, DateTimeFormats.SQ_DATE_TIME, null);
                DateTime endDateTime = DateTime.ParseExact(end, DateTimeFormats.SQ_DATE_TIME, null);
                string dayOfTheWeek = startDateTime.DayOfWeek.ToString();
                string startKey = $"{dayOfTheWeek}StartTime";
                string endKey = $"{dayOfTheWeek}EndTime";
                DateTime workingHoursStart = (DateTime) employeeWorkingHours.GetType()
                                                                            .GetProperty(startKey)
                                                                            .GetValue(employeeWorkingHours, null);
                DateTime workingHoursEnd = (DateTime) employeeWorkingHours.GetType()
                                                                            .GetProperty(endKey)
                                                                            .GetValue(employeeWorkingHours, null);

                if (
                    (startDateTime.TimeOfDay >= workingHoursStart.TimeOfDay)
                        && (endDateTime.TimeOfDay <= workingHoursEnd.TimeOfDay)
                ) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception e) {
                return false;
            }
        }

        private async Task<bool> checkIfBusy(
            string employeeType,
            string startDateTime,
            string endDateTime,
            int id
        ) {
            try {
                string queryForDoctors = $@"
                        SELECT
                            COUNT(*)
                        FROM 
                            {DBTables.RESERVATION} 
                        WHERE
                            {DBTables.RESERVATION}.doctor = @id
                            AND (
                                {DBTables.RESERVATION}.start_datetime BETWEEN @start AND @end
                                OR
                                {DBTables.RESERVATION}.end_datetime BETWEEN @start AND @end
                            )";
                string queryForNurses = $@"
                        SELECT 
                            COUNT(*)
                        FROM 
                            {DBTables.RESERVATION} 
                        WHERE
                            {DBTables.RESERVATION}.nurse = @id
                            AND (
                                ({DBTables.RESERVATION}.start_datetime BETWEEN @start AND @end)
                                OR
                                ({DBTables.RESERVATION}.end_datetime BETWEEN @start AND @end)
                            )";
                string query = employeeType == "doctor" ? queryForDoctors : queryForNurses;

                MySqlConnection connection = new MySqlConnection(DB.connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@start", startDateTime);
                cmd.Parameters.AddWithValue("@end", endDateTime);
                cmd.Prepare();

                int resultCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                return resultCount == 0;
            } catch (Exception e) {
                throw e;
            }
        }

        public async Task<long> createReservation(
            string startDateTime,
            string endDateTime,
            int service,
            int createdBy,
            int patient,
            int doctor,
            int doctorEmployeeId,
            int nurse,
            int nurseEmployeeId
        ) {
            try {
                bool startDateTimeIsValid = startDateTime != null && startDateTime.Length > 0;
                bool endDateTimeIsValid = endDateTime != null && endDateTime.Length > 0;
                bool serviceIsValid = service > 0;
                bool createdByIsValid = createdBy > 0;
                bool patientIsValid = patient > 0;
                bool doctorIsValid = doctor > 0;
                bool nurseIsValid = nurse > 0;

                if (
                    startDateTimeIsValid
                        && endDateTimeIsValid
                        && serviceIsValid
                        && createdByIsValid
                        && patientIsValid
                        && doctorIsValid
                        && nurseIsValid
                ) {
                    DateTime start = DateTime.ParseExact(startDateTime, DateTimeFormats.SQ_DATE_TIME, null);
                    DateTime end = DateTime.ParseExact(endDateTime, DateTimeFormats.SQ_DATE_TIME, null);
                    string startSQLFormat = start.ToString(DateTimeFormats.MYSQL_DATE_TIME);
                    string endSQLFormat = end.ToString(DateTimeFormats.MYSQL_DATE_TIME);

                    // Check if the reservation is withing doctor's and nurse's working hours
                    bool doctorIsWorking = await checkIfEmployeeIsWorking(doctorEmployeeId, startDateTime, endDateTime);
                    bool nurseIsWorking = await checkIfEmployeeIsWorking(nurseEmployeeId, startDateTime, endDateTime);

                    if (!doctorIsWorking) {
                        throw new Exception("Rezervimi është jashtë orarit të punës së mjekut");
                    }
                    if (!nurseIsWorking) {
                        throw new Exception("Rezervimi është jashtë orarit të punës së infermierit");
                    }

                    // Check if doctor or nurse are busy on other appointments
                    bool doctorIsBusy = await checkIfBusy("doctor", startSQLFormat, endSQLFormat, doctor);
                    bool nurseIsBusy = await checkIfBusy("nurse", startSQLFormat, startSQLFormat, nurse);

                    // Create reservation
                    string query = $@"
                        INSERT INTO
                            {DBTables.RESERVATION}
                        VALUES (
                            null,
                            @startDateTime,
                            @endDateTime,
                            @service,
                            @createdBy,
                            @patient,
                            @nurse,
                            @doctor
                        )";

                    MySqlConnection connection = new MySqlConnection(DB.connectionString);
                    connection.Open();

                    int operatorId = await new Operator().getOperatorByUserId(createdBy);

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@startDateTime", startSQLFormat);
                    cmd.Parameters.AddWithValue("@endDateTime", endSQLFormat);
                    cmd.Parameters.AddWithValue("@service", service);
                    cmd.Parameters.AddWithValue("@createdBy", operatorId);
                    cmd.Parameters.AddWithValue("@patient", patient);
                    cmd.Parameters.AddWithValue("@nurse", nurse);
                    cmd.Parameters.AddWithValue("@doctor", doctor);
                    cmd.Prepare();

                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                    return cmd.LastInsertedId;
                } else {
                    throw new Exception("Input i gabuar ose i pamjaftueshëm");
                }
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

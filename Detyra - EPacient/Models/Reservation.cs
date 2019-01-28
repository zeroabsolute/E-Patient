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
                                {DBTables.NURSE}.employee = {DBTables.EMPLOYEE}.id
                            )
                            ON
                            {DBTables.RESERVATION}.nurse = {DBTables.NURSE}.id
                            INNER JOIN (
                                {DBTables.DOCTOR} as doctor
                                INNER JOIN
                                {DBTables.DOCTOR} as doctorEmployee
                                ON
                                {DBTables.DOCTOR}.employee = {DBTables.EMPLOYEE}.id
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
    }
}

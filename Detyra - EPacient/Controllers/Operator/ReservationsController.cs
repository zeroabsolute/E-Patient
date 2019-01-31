using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Operator;

namespace Detyra___EPacient.Controllers.Operator {
    class ReservationsController {
        private Reservations view;

        private Models.Reservation reservationModel;
        private Models.Service serviceModel;
        private Models.Patient patientModel;
        private Models.Doctor doctorModel;
        private Models.Nurse nurseModel;
        private List<Models.Reservation> reservations;
        private List<Models.Doctor> doctors;
        private List<Models.Nurse> nurses;

        public ReservationsController(Reservations view) {
            this.view = view;
            this.reservationModel = new Models.Reservation();
            this.serviceModel = new Models.Service();
            this.patientModel = new Models.Patient();
            this.doctorModel = new Models.Doctor();
            this.nurseModel = new Models.Nurse();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read reservations from DB and populate table
                List<Models.Reservation> reservations = await this.reservationModel.readReservations();
                this.reservations = reservations;

                this.view.ReservationsTable.DataGrid.Rows.Clear();
                this.view.ReservationsTable.DataGrid.Refresh();

                reservations.ForEach((item) => {
                    this.view.ReservationsTable.DataGrid.Rows.Add(
                        item.Id,
                        item.StartDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
                        item.EndDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
                        $"{item.Patient.FirstName} {item.Patient.LastName}",
                        $"{item.Doctor.Employee.FirstName} {item.Doctor.Employee.LastName}",
                        $"{item.Nurse.Employee.FirstName} {item.Nurse.Employee.LastName}",
                        $"{item.Service.Name}"
                    );
                });

                // Read services and populate combo box
                List<Models.Service> services = await serviceModel.readServices();

                this.view.ServiceCBox.comboBox.DisplayMember = "name";
                this.view.ServiceCBox.comboBox.ValueMember = "id";
                this.view.ServiceCBox.comboBox.DataSource = services;

                // Read patients and populate combo box
                List<Models.Patient> patients = await patientModel.readPatients();

                this.view.PatientCBox.comboBox.DisplayMember = "fullname";
                this.view.PatientCBox.comboBox.ValueMember = "id";
                this.view.PatientCBox.comboBox.DataSource = patients;

                // Read doctors and populate combo box
                this.doctors = await doctorModel.readDoctors();

                this.view.DoctorCBox.comboBox.DisplayMember = "fullname";
                this.view.DoctorCBox.comboBox.ValueMember = "id";
                this.view.DoctorCBox.comboBox.DataSource = doctors;

                // Read nurses and populate combo box
                this.nurses = await nurseModel.readNurses();

                this.view.NurseCBox.comboBox.DisplayMember = "fullname";
                this.view.NurseCBox.comboBox.ValueMember = "id";
                this.view.NurseCBox.comboBox.DataSource = nurses;

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle selection of a table row
         */

        public void handleTableRowSelection() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                var selectedRow = this.view.ReservationsTable.DataGrid.SelectedRows.Count > 0
                ? this.view.ReservationsTable.DataGrid.SelectedRows[0]
                : null;

                if (selectedRow != null) {
                    int id = (int) selectedRow.Cells[0].Value;

                    this.reservations.ForEach((item) => {
                        if (item.Id == id) {
                            this.view.SelectedReservation = item;
                            this.view.SelectedReservationLabel.Text = item.Id.ToString();
                            this.view.StartDateTime.Value = item.StartDateTime;
                            this.view.EndDateTime.Value = item.EndDateTime;
                            this.view.ServiceCBox.comboBox.SelectedValue = item.Service.Id;
                            this.view.PatientCBox.comboBox.SelectedValue = item.Patient.Id;
                            this.view.DoctorCBox.comboBox.SelectedValue = item.Doctor.Id;
                            this.view.NurseCBox.comboBox.SelectedValue = item.Nurse.Id;
                        }
                    });
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle search for a reservation
         */

        public void handleSearch() {

        }

        /**
         * Controller to handle printing a reservation.
         */
        
        public void handlePrintReservation() {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(this.view.SelectedReservation.Id);
            Console.WriteLine("-------------------------------------------");
        }

        /**
         * Controller to handle printing a reservation fee.
         */

        public void handlePrintFee() {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(this.view.SelectedReservation.Service.Fee);
            Console.WriteLine("-------------------------------------------");
        }

        /**
         * Controller to handle reset button
         */

        public void handleResetButton() {
            this.view.SelectedReservation = null;
            this.view.SelectedReservationLabel.Text = "-";
        }

        /**
         * Controller to handle submit button
         */

        private int getNurseEmployeeId() {
            int selectedNurse = (int) this.view.NurseCBox.comboBox.SelectedValue;
            int idToReturn = -1;

            this.nurses.ForEach((item) => {
                if (item.Id == selectedNurse) {
                    idToReturn = item.Employee.Id;
                }
            });

            return idToReturn;
        }

        private int getDoctorEmployeeId() {
            int selectedDoctor = (int) this.view.DoctorCBox.comboBox.SelectedValue;
            int idToReturn = -1;

            this.doctors.ForEach((item) => {
                if (item.Id == selectedDoctor) {
                    idToReturn = item.Employee.Id;
                }
            });

            return idToReturn;
        }

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (this.view.SelectedReservation == null) {
                    await reservationModel.createReservation(
                        this.view.StartDateTime.Value.ToString(DateTimeFormats.SQ_DATE_TIME),
                        this.view.EndDateTime.Value.ToString(DateTimeFormats.SQ_DATE_TIME),
                        (int) this.view.ServiceCBox.comboBox.SelectedValue,
                        this.view.LoggedInUser.Id,
                        (int) this.view.PatientCBox.comboBox.SelectedValue,
                        (int) this.view.DoctorCBox.comboBox.SelectedValue,
                        this.getDoctorEmployeeId(),
                        (int) this.view.NurseCBox.comboBox.SelectedValue,
                        this.getNurseEmployeeId()
                    );

                    MessageBox.Show("Rezervimi u krye me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    await reservationModel.updateReservation(
                        this.view.SelectedReservation.Id,
                        this.view.StartDateTime.Value.ToString(DateTimeFormats.SQ_DATE_TIME),
                        this.view.EndDateTime.Value.ToString(DateTimeFormats.SQ_DATE_TIME),
                        (int) this.view.ServiceCBox.comboBox.SelectedValue,
                        (int) this.view.PatientCBox.comboBox.SelectedValue,
                        (int) this.view.DoctorCBox.comboBox.SelectedValue,
                        this.getDoctorEmployeeId(),
                        (int) this.view.NurseCBox.comboBox.SelectedValue,
                        this.getNurseEmployeeId()
                    );

                    MessageBox.Show("Rezervimi u përditësua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.handleResetButton();
                this.init();

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

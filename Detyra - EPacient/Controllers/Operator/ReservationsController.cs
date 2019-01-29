using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                        item.StartDateTime.ToString("dd-MM-yyyy HH:mm"),
                        item.EndDateTime.ToString("dd-MM-yyyy HH:mm"),
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
                List<Models.Doctor> doctors = await doctorModel.readDoctors();

                this.view.DoctorCBox.comboBox.DisplayMember = "fullname";
                this.view.DoctorCBox.comboBox.ValueMember = "id";
                this.view.DoctorCBox.comboBox.DataSource = doctors;

                // Read nurses and populate combo box
                List<Models.Nurse> nurses = await nurseModel.readNurses();

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
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

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

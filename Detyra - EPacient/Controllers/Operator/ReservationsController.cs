using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Styles;
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
         * Helper to populate table with data
         */

        private void populateTable(List<Models.Reservation> data) {
            this.view.ReservationsTable.DataGrid.Rows.Clear();
            this.view.ReservationsTable.DataGrid.Refresh();

            data.ForEach((item) => {
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
                this.populateTable(reservations);

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
            string searchTerm = this.view.SearchTermTxtBox.Text;
            List<Models.Reservation> filteredReservations = new List<Models.Reservation>();

            if (searchTerm.Length == 0) {
                this.populateTable(this.reservations);

                return;
            }

            if (this.reservations != null && this.reservations.Count > 0) {
                this.reservations.ForEach((item) => {
                    string patientName = item.Patient.FullName;

                    if (patientName.StartsWith(searchTerm)) {
                        filteredReservations.Add(item);
                    }
                });

                this.populateTable(filteredReservations);
            }
        }

        /**
         * Controller to handle printing a reservation.
         */

        private void openPrintDialog() {
            using (PrintDialog printDialog = new PrintDialog()) {
                if (printDialog.ShowDialog() == DialogResult.OK) {
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintPage += this.printReservation;
                    printDocument.Print();
                }
            }
        }

        private void printReservation(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            var reservation = this.view.SelectedReservation;

            e.Graphics.DrawString(
                "EPacient - Rezervimi",
                new Font(Fonts.primary, 20, FontStyle.Bold), 
                Brushes.SaddleBrown, 
                new Point(300, 100)
            );
            e.Graphics.DrawString(
                "ID e rezervimit",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray, 
                new Point(80, 200)
            );
            e.Graphics.DrawString(
                $"{this.view.SelectedReservation.Id}",
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 200)
            );
            e.Graphics.DrawString(
                "Pacienti",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 250)
            );
            e.Graphics.DrawString(
                this.view.SelectedReservation.Patient.FullName,
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 250)
            );
            e.Graphics.DrawString(
                "Data dhe ora e fillimit",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray, 
                new Point(80, 300)
            );
            e.Graphics.DrawString(
                this.view.SelectedReservation.StartDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 300)
            );
            e.Graphics.DrawString(
                $"Data dhe ora e përfundimit",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 350)
            );
            e.Graphics.DrawString(
                this.view.SelectedReservation.EndDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 350)
            );
            e.Graphics.DrawString(
                "Mjeku",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 400)
            );
            e.Graphics.DrawString(
                this.view.SelectedReservation.Doctor.FullName,
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 400)
            );
            e.Graphics.DrawString(
                "Infermieri",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 450)
            );
            e.Graphics.DrawString(
                this.view.SelectedReservation.Nurse.FullName,
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 450)
            );
            e.Graphics.DrawString(
                "Shërbimi",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 500)
            );
            e.Graphics.DrawString(
                this.view.SelectedReservation.Service.Name,
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(500, 500)
            );
            e.Graphics.DrawString(
                "Totali i kostos",
                new Font(Fonts.primary, 14, FontStyle.Bold),
                Brushes.Black,
                new Point(80, 600)
            );
            e.Graphics.DrawString(
                $"{this.view.SelectedReservation.Service.Fee} Lekë",
                new Font(Fonts.primary, 14, FontStyle.Bold),
                Brushes.DarkBlue,
                new Point(500, 600)
            );
            e.Graphics.DrawRectangle(
                new Pen(Color.Black, 1), 
                new Rectangle(50, 80, 730, 580)
            );
        }

        public void handlePrintReservation() {
            if (this.view.SelectedReservation == null) {
                MessageBox.Show("Nuk ka rezervim të zgjedhur!", "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                this.openPrintDialog();
            }
        }

        /**
         * Controller to handle editing a reservation.
         */

        public void handleEdit() {
            if (this.view.SelectedReservation == null) {
                MessageBox.Show("Nuk ka rezervim të zgjedhur!", "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                this.view.SelectedReservationLabel.Text = this.view.SelectedReservation.Id.ToString();
                this.view.StartDateTime.Value = this.view.SelectedReservation.StartDateTime;
                this.view.EndDateTime.Value = this.view.SelectedReservation.EndDateTime;
                this.view.ServiceCBox.comboBox.SelectedValue = this.view.SelectedReservation.Service.Id;
                this.view.PatientCBox.comboBox.SelectedValue = this.view.SelectedReservation.Patient.Id;
                this.view.DoctorCBox.comboBox.SelectedValue = this.view.SelectedReservation.Doctor.Id;
                this.view.NurseCBox.comboBox.SelectedValue = this.view.SelectedReservation.Nurse.Id;
            }
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

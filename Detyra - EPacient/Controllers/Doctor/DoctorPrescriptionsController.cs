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
using Detyra___EPacient.Views.Doctor;

namespace Detyra___EPacient.Controllers.Doctor {
    class PrescriptionsController {
        private Prescription view;

        private Models.Reservation reservationModel;
        private Models.Reservation selectedReservation;
        private Models.Doctor doctorModel;
        private Models.Medicament medicamentModel;
        private List<Models.Reservation> reservations;
        private List<Models.Medicament> medicaments;

        public PrescriptionsController(Prescription view) {
            this.view = view;
            this.reservationModel = new Models.Reservation();
            this.doctorModel = new Models.Doctor();
            this.medicamentModel = new Models.Medicament();
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
                int id = await doctorModel.getDoctorByUserId(this.view.LoggedInUser.Id);

                List<Models.Reservation> reservations = await this.reservationModel.readReservationsForDoctors(id);
                this.reservations = reservations;
                this.populateTable(reservations);

                // Read medicaments
                this.medicaments = await medicamentModel.readMedicaments();

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
                            this.selectedReservation = item;
                            this.view.SelectedReservationLabel.Text = this.selectedReservation.Id.ToString();

                            // Populate list box with medicaments
                            this.view.MedicamentsListBox.Items.Clear();
                            this.view.MedicamentsListBox.Refresh();

                            if (this.medicaments != null && this.medicaments.Count > 0) {
                                this.medicaments.ForEach((medicament) => {
                                    this.view.MedicamentsListBox.Items.Add(medicament);
                                });
                            }
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
         * Controller to handle printing a prescription.
         */

        private void openPrintDialog() {
            using (PrintDialog printDialog = new PrintDialog()) {
                if (printDialog.ShowDialog() == DialogResult.OK) {
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintPage += this.printPrescription;
                    printDocument.Print();
                }
            }
        }

        private void printPrescription(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            var reservation = this.selectedReservation;

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
                $"{this.selectedReservation.Id}",
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
                this.selectedReservation.Patient.FullName,
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
                this.selectedReservation.StartDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
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
                this.selectedReservation.EndDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
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
                this.selectedReservation.Doctor.FullName,
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
                this.selectedReservation.Nurse.FullName,
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
                this.selectedReservation.Service.Name,
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
                $"{this.selectedReservation.Service.Fee} Lekë",
                new Font(Fonts.primary, 14, FontStyle.Bold),
                Brushes.DarkBlue,
                new Point(500, 600)
            );
            e.Graphics.DrawRectangle(
                new Pen(Color.Black, 1), 
                new Rectangle(50, 80, 730, 580)
            );
        }

        public void handlePrintPrescription() {
            if (this.selectedReservation == null) {
                MessageBox.Show("Nuk ka rezervim të zgjedhur!", "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                this.openPrintDialog();
            }
        }

        /**
         * Controller to handle reset button
         */

        public void handleResetButton() {
            this.selectedReservation = null;
            this.view.SelectedReservationLabel.Text = "-";
            this.view.DescriptionTxtBox.Text = "";
        }

        /**
         * Controller to handle submit button
         */

        public void handleSubmitButton() {
            try {

            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

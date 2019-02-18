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
        private Models.Prescription selectedPrescription;
        private List<Models.PrescriptionMedicament> selectedPrescriptionMedicaments;
        private Models.Allergen allergenModel;
        private Models.Doctor doctorModel;
        private Models.Medicament medicamentModel;
        private Models.Prescription prescriptionModel;
        private Models.PrescriptionMedicament prescriptionMedicamentModel;
        private List<Models.Reservation> reservations;
        private List<Models.Medicament> medicaments;

        public PrescriptionsController(Prescription view) {
            this.view = view;
            this.reservationModel = new Models.Reservation();
            this.doctorModel = new Models.Doctor();
            this.allergenModel = new Models.Allergen();
            this.medicamentModel = new Models.Medicament();
            this.prescriptionModel = new Models.Prescription();
            this.prescriptionMedicamentModel = new Models.PrescriptionMedicament();
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

                this.handleResetButton();

                var selectedRow = this.view.ReservationsTable.DataGrid.SelectedRows.Count > 0
                    ? this.view.ReservationsTable.DataGrid.SelectedRows[0]
                    : null;

                if (selectedRow != null) {
                    int id = (int) selectedRow.Cells[0].Value;

                    this.reservations.ForEach(async (item) => {
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

                            // Read prescription for selected reservation
                            this.selectedPrescription = await prescriptionModel.readPrescriptionForReservation(
                                this.selectedReservation.Id
                            );

                            // If there is an existing prescription, show its content
                            if (this.selectedPrescription != null) {
                                List<Models.PrescriptionMedicament> pm = await prescriptionMedicamentModel.readMedicamentsForPrescription(
                                    this.selectedPrescription.Id
                                );
                                this.selectedPrescriptionMedicaments = pm;

                                this.view.DescriptionTxtBox.ReadOnly = true;
                                this.view.MedicamentsListBox.Enabled = false;
                                this.view.SubmitBtn.Enabled = false;
                                this.view.DescriptionTxtBox.Text = this.selectedPrescription.Description;

                                for (int i = 0; i < this.view.MedicamentsListBox.Items.Count; i += 1) {
                                    Models.Medicament m = (Models.Medicament) this.view.MedicamentsListBox.Items[i];

                                    pm.ForEach((pmItem) => {
                                        if (pmItem.Medicament == m.Id) {
                                            this.view.MedicamentsListBox.SetSelected(i, true);
                                        }
                                    });
                                }
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

        private string medicamentsToString() {
            if (this.selectedPrescriptionMedicaments != null && this.selectedPrescriptionMedicaments.Count > 0) {
                string medicaments = "";

                this.selectedPrescriptionMedicaments.ForEach((pm) => {
                    this.medicaments.ForEach((m) => {
                        if (pm.Medicament == m.Id) {
                            medicaments = $"{medicaments}{(medicaments.Length > 0 ? ", " : "")}{m.Name}";
                        }
                    });
                });

                return medicaments;
            } else {
                return "-";
            }
        }

        private void printPrescription(object sender, PrintPageEventArgs e) {
            var reservation = this.selectedReservation;

            e.Graphics.DrawString(
                "EPacient - Recetë",
                new Font(Fonts.primary, 20, FontStyle.Bold), 
                Brushes.SaddleBrown, 
                new Point(300, 100)
            );
            e.Graphics.DrawString(
                "ID e recetës",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray, 
                new Point(80, 200)
            );
            e.Graphics.DrawString(
                $"{this.selectedPrescription.Id}",
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(300, 200)
            );
            e.Graphics.DrawString(
                "Data",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 250)
            );
            e.Graphics.DrawString(
                DateTime.Now.ToString(DateTimeFormats.SQ_DATE_TIME),
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(300, 250)
            );
            e.Graphics.DrawString(
                "Pacienti",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 300)
            );
            e.Graphics.DrawString(
                this.selectedReservation.Patient.FullName,
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(300, 300)
            );
            e.Graphics.DrawString(
                "Mjeku",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 350)
            );
            e.Graphics.DrawString(
                this.selectedReservation.Doctor.FullName,
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(300, 350)
            );
            e.Graphics.DrawString(
                "Medikamentet",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 400)
            );
            e.Graphics.DrawString(
                this.medicamentsToString(),
                new Font(Fonts.primary, 12, FontStyle.Bold),
                Brushes.Black,
                new Point(300, 400)
            );
            e.Graphics.DrawString(
                "Përshkrimi",
                new Font(Fonts.primary, 12, FontStyle.Regular),
                Brushes.Gray,
                new Point(80, 450)
            );
            for (int i = 0; i < this.view.DescriptionTxtBox.Text.Length; i += 1) {
                if (i % 50 == 0 && (i / 50 > 0)) {
                    e.Graphics.DrawString(
                        this.view.DescriptionTxtBox.Text.Substring(i - 50, 50),
                        new Font(Fonts.primary, 12, FontStyle.Bold),
                        Brushes.Black,
                        new Point(300, 400 + (50 * (i / 50)))
                    );
                }
            }
        }

        public void handlePrintPrescription() {
            if (this.selectedReservation == null || this.selectedPrescription == null) {
                MessageBox.Show("Nuk ka recetë të zgjedhur!", "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                this.openPrintDialog();
            }
        }

        /**
         * Controller to handle reset button
         */

        public void handleResetButton() {
            this.view.SubmitBtn.Enabled = true;
            this.view.DescriptionTxtBox.ReadOnly = false;
            this.view.MedicamentsListBox.Enabled = true;
            this.selectedReservation = null;
            this.view.SelectedReservationLabel.Text = "-";
            this.view.DescriptionTxtBox.Text = "";
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                if (this.selectedReservation == null) {
                    string message = "Nuk është zgjedhur asnjë rezervim";
                    MessageBox.Show(message, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                // Extract selected medicaments
                List<Models.Medicament> selectedMedicaments = new List<Models.Medicament>();

                for (int i = 0; i < this.view.MedicamentsListBox.SelectedItems.Count; i += 1) {
                    Models.Medicament medicament = (Models.Medicament) this.view.MedicamentsListBox.SelectedItems[i];
                    selectedMedicaments.Add(medicament);
                }

                // Check for allergens
                int patientId = this.selectedReservation.Patient.Id;
                List<Models.Allergen> allergens = await this.allergenModel.readAllergensForPatient(patientId);
                string patientAllergens = "";

                if (allergens != null && allergens.Count > 0) {
                    allergens.ForEach((item) => {
                        for (int i = 0; i < selectedMedicaments.Count; i += 1) {
                            if (item.Medicament.Id == selectedMedicaments[i].Id) {
                                patientAllergens = $"{patientAllergens}{(patientAllergens.Length > 0 ? ", " : "")}{selectedMedicaments[i].Name}";
                            }
                        }
                    });
                }

                if (patientAllergens.Length > 0) {
                    string message = $"Pacienti është alergjik ndaj medikamenteve të mëposhtme:\n{patientAllergens}";
                    MessageBox.Show(message, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                // If no allergens, create prescription and prescription medicaments
                long prescriptionId = await prescriptionModel.createPrescription(
                    this.view.DescriptionTxtBox.Text,
                    this.selectedReservation.Id
                );
                await prescriptionMedicamentModel.createPrescriptionMedicaments(
                    prescriptionId,
                    selectedMedicaments
                );

                string message2 = "Receta u shtua me sukses";
                MessageBox.Show(message2, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.handleTableRowSelection();
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

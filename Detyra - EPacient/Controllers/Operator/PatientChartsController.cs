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
using Detyra___EPacient.Views.Operator.PatientCharts;

namespace Detyra___EPacient.Controllers.Operator {
    public class PatientChartsController {
        private OperatorPatientCharts view;
        private AddDocsForm addDocsForm;
        private AddAllergensForm addAllergensForm;
        private Models.Patient patientModel;
        private Models.PatientChart patientChartModel;
        private Models.ChartDocument chartDocumentModel;
        private Models.Allergen allergenModel;
        private List<Models.Patient> patients;
        public List<Models.Medicament> medicaments;
        private Models.PatientChart selectedChart;

        // Printer related
        private int counter;
        private int pageCount;
        private int pageSize = 8;

        public PatientChartsController(OperatorPatientCharts view) {
            this.view = view;
            this.patientModel = new Models.Patient();
            this.patientChartModel = new Models.PatientChart();
            this.chartDocumentModel = new Models.ChartDocument();
            this.allergenModel = new Models.Allergen();
            this.counter = 0;
            this.pageCount = 1;
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read patients from DB and populate table
                List<Models.Patient> patients = await this.patientModel.readPatients();
                this.patients = patients;

                // Read medicaments
                this.medicaments = await new Models.Medicament().readMedicaments();

                this.view.PatientsTable.DataGrid.Rows.Clear();
                this.view.PatientsTable.DataGrid.Refresh();

                patients.ForEach((item) => {
                    this.view.PatientsTable.DataGrid.Rows.Add(
                        item.Id,
                        item.FirstName,
                        item.LastName,
                        item.DateOfBirth.ToString(DateTimeFormats.SQ_DATE),
                        item.Gender
                    );
                });

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

                var selectedRow = this.view.PatientsTable.DataGrid.SelectedRows.Count > 0
                ? this.view.PatientsTable.DataGrid.SelectedRows[0]
                : null;

                if (selectedRow != null) {
                    int id = (int) selectedRow.Cells[0].Value;

                    this.patients.ForEach(async (item) => {
                        if (item.Id == id) {
                            this.view.SelectedPatient = item;
                            this.view.PatientLabelValue.Text = item.FullName;

                            this.selectedChart = await this.readPatientChart();
                            await this.readPatientChartDocs(this.selectedChart.Id);
                            await this.readAllergens(this.selectedChart.Id);
                        }
                    });
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Models.PatientChart> readPatientChart() {
            try {
                if (this.view.SelectedPatient == null) {
                    throw new Exception("Nuk është zgjedhur asnjë pacient");
                }

                Cursor.Current = Cursors.WaitCursor;

                Models.PatientChart chart = await patientChartModel.readPatientChart(this.view.SelectedPatient.Id);

                if (chart == null) {
                    throw new Exception("Nuk u gjet asnjë kartelë");
                }

                return chart;
            } catch (Exception e) {
                throw e;
            }
        }

        private async Task<List<Models.ChartDocument>> readPatientChartDocs(int chartId) {
            try {
                List<Models.ChartDocument> docs = await chartDocumentModel.readChartDocuments(chartId);

                this.view.DocsTable.DataGrid.Rows.Clear();
                this.view.DocsTable.DataGrid.Refresh();

                docs.ForEach((item) => {
                    this.view.DocsTable.DataGrid.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Type,
                        item.URL,
                        item.DateCreated.ToString(DateTimeFormats.SQ_DATE_TIME)
                    );
                });

                return docs;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        private async Task<List<Models.Allergen>> readAllergens(int chartId) {
            try {
                List<Models.Allergen> allergens = await allergenModel.readAllergens(chartId);

                this.view.AllergensTable.DataGrid.Rows.Clear();
                this.view.AllergensTable.DataGrid.Refresh();

                allergens.ForEach((item) => {
                    this.view.AllergensTable.DataGrid.Rows.Add(
                        item.Id,
                        item.Medicament.Name
                    );
                });

                return allergens;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        /**
         * Handle adding docs
         */

        public void handleAddDoc() {
            if (this.selectedChart == null) {
                string message = "Nuk është zgjedhur asnjë pacient";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            this.addDocsForm = new AddDocsForm(this);
            this.addDocsForm.Show();
        }

        public async void handleDocSubmit(string url, string name, string type) {
            try {
                Cursor.Current = Cursors.WaitCursor;

                await chartDocumentModel.createChartDocument(
                    name,
                    type,
                    url,
                    DateTime.Now.ToString(DateTimeFormats.MYSQL_DATE_TIME),
                    this.selectedChart.Id
                );

                this.addDocsForm.Hide();
                await this.readPatientChartDocs(this.selectedChart.Id);

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Handle adding allergens
         */

        public void handleAddAllergen() {
            if (this.selectedChart == null) {
                string message = "Nuk është zgjedhur asnjë pacient";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            this.addAllergensForm = new AddAllergensForm(this);
            this.addAllergensForm.Show();
        }

        public async void handleAllergenSubmit(int medicamentId) {
            try {
                Cursor.Current = Cursors.WaitCursor;

                await allergenModel.createAllergen(
                    this.selectedChart.Id,
                    medicamentId
                );

                this.addAllergensForm.Hide();
                await this.readAllergens(this.selectedChart.Id);

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Print a patient chart
         */

        public void handlePrintChart() {
            if (this.selectedChart == null) {
                string message = "Nuk është zgjedhur asnjë pacient";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            using (PrintDialog printDialog = new PrintDialog()) {
                if (printDialog.ShowDialog() == DialogResult.OK) {
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintPage += this.printChart;
                    printDocument.Print();
                }
            }
        }

        private async void printChart(object sender, PrintPageEventArgs e) {
            try {
                Cursor.Current = Cursors.WaitCursor;

                List<Models.Allergen> allergens = await this.readAllergens(this.selectedChart.Id);
                List<Models.ChartDocument> docs = await this.readPatientChartDocs(this.selectedChart.Id);
                string allergensResult = "";

                allergens.ForEach((item) => {
                    allergensResult = $"{allergensResult}{(allergensResult.Length > 0 ? ", " : "")}{item.Medicament.Name}";
                });

                // Printing

                int startY = 50;
                int margin = 30;
                int keyX = 80;
                int valueX = 400;
                int itemFontSize = 10;
                int headerFontSize = 14;

                if (this.pageCount == 1) {
                    e.Graphics.DrawString(
                    "EPacient - Kartela e pacientit",
                    new Font(Fonts.primary, 20, FontStyle.Bold),
                    Brushes.SaddleBrown,
                    new Point(250, startY)
                );
                    e.Graphics.DrawString(
                        "Gjeneralitetet",
                        new Font(Fonts.primary, headerFontSize, FontStyle.Bold),
                        Brushes.DarkBlue,
                        new Point(keyX, (startY + (int) (2.5 * margin)))
                    );
                    e.Graphics.DrawString(
                        "ID",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 4 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.Id}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 4 * margin))
                    );
                    e.Graphics.DrawString(
                        "Emri",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 5 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.FirstName}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 5 * margin))
                    );
                    e.Graphics.DrawString(
                        "Mbiemri",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 6 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.LastName}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 6 * margin))
                    );
                    e.Graphics.DrawString(
                        "Gjinia",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 7 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.Gender}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 7 * margin))
                    );
                    e.Graphics.DrawString(
                        "Datëlindja",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 8 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.DateOfBirth.ToString(DateTimeFormats.SQ_DATE)}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 8 * margin))
                    );
                    e.Graphics.DrawString(
                        "Celular",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 9 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.PhoneNumber}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 9 * margin))
                    );
                    e.Graphics.DrawString(
                        "Adresa",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 10 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{this.view.SelectedPatient.Address}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 10 * margin))
                    );
                    e.Graphics.DrawString(
                        "Alergjitë e pacientit",
                        new Font(Fonts.primary, headerFontSize, FontStyle.Bold),
                        Brushes.DarkBlue,
                        new Point(keyX, (startY + (int) (12.5 * margin)))
                    );
                    e.Graphics.DrawString(
                        "Medikamente alergenë",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + 14 * margin))
                    );
                    e.Graphics.DrawString(
                        $"{(allergensResult.Length > 0 ? allergensResult : "-")}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + 14 * margin))
                    );
                    e.Graphics.DrawString(
                        "Dokumentacioni dhe historiku i pacientit",
                        new Font(Fonts.primary, headerFontSize, FontStyle.Bold),
                        Brushes.DarkBlue,
                        new Point(keyX, (startY + (int) (16.5 * margin)))
                    );
                }

                int nextY = this.pageCount == 1 ? 18 : 0;

                for (int i = this.counter; i < docs.Count; i += 1) {
                    Models.ChartDocument item = docs[i];

                    e.Graphics.DrawString(
                        "Lloji i dokumentit",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + nextY * margin))
                    );
                    e.Graphics.DrawString(
                        $"{item.Type}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + nextY * margin))
                    );
                    e.Graphics.DrawString(
                        "Emri",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + (1 + nextY) * margin))
                    );
                    e.Graphics.DrawString(
                        $"{item.Name}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + (1 + nextY) * margin))
                    );
                    e.Graphics.DrawString(
                        "Data e krijimit",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Regular),
                        Brushes.Gray,
                        new Point(keyX, (startY + (2 + nextY) * margin))
                    );
                    e.Graphics.DrawString(
                        $"{item.DateCreated.ToString(DateTimeFormats.SQ_DATE_TIME)}",
                        new Font(Fonts.primary, itemFontSize, FontStyle.Bold),
                        Brushes.Black,
                        new Point(valueX, (startY + (2 + nextY) * margin))
                    );

                    if (this.pageCount == 1 && this.counter == 3) {
                        e.HasMorePages = true;
                        this.pageCount += 1;
                        nextY = 0;

                        break;
                    } else if (this.counter == (3 + (this.pageCount - 1) * this.pageSize)) {
                        e.HasMorePages = true;
                        this.pageCount += 1;
                        nextY = 0;

                        break;
                    } else {
                        nextY += 4;
                    }

                    this.counter += 1;
                }

                this.counter = 0;
                Cursor.Current = Cursors.Arrow;
            } catch (Exception ex) {
                string caption = "Problem në lexim";
                MessageBox.Show(ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Operator;
using Detyra___EPacient.Views.Operator.PatientCharts;

namespace Detyra___EPacient.Controllers.Operator {
    public class PatientChartsController {
        private OperatorPatientCharts view;
        private AddDocsForm addDocsForm;
        private Models.Patient patientModel;
        private Models.PatientChart patientChartModel;
        private Models.ChartDocument chartDocumentModel;
        private Models.Allergen allergenModel;
        private List<Models.Patient> patients;
        private Models.PatientChart selectedChart;

        public PatientChartsController(OperatorPatientCharts view) {
            this.view = view;
            this.patientModel = new Models.Patient();
            this.patientChartModel = new Models.PatientChart();
            this.chartDocumentModel = new Models.ChartDocument();
            this.allergenModel = new Models.Allergen();
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
                            this.readPatientChartDocs(this.selectedChart.Id);
                            this.readAllergens(this.selectedChart.Id);
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

        private async void readPatientChartDocs(int chartId) {
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
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void readAllergens(int chartId) {
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
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Handle clicking on add docs button
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
                this.readPatientChartDocs(this.selectedChart.Id);

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

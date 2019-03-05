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
using Detyra___EPacient.Views.Nurse;
using Detyra___EPacient.Views.Nurse.Analysis;

namespace Detyra___EPacient.Controllers.Nurse {
    public class AnalysisController {
        private AnalysisNurse view;
        private AddAnalysisForm addAnalysisForm;
        private Models.Patient patientModel;
        private Models.PatientChart patientChartModel;
        private Models.ChartDocument chartDocumentModel;
        private List<Models.Patient> patients;
        private Models.PatientChart selectedChart;

        public AnalysisController(AnalysisNurse view) {
            this.view = view;
            this.patientModel = new Models.Patient();
            this.patientChartModel = new Models.PatientChart();
            this.chartDocumentModel = new Models.ChartDocument();
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
            }
            catch (Exception e) {
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
                    int id = (int)selectedRow.Cells[0].Value;

                    this.patients.ForEach(async (item) => {
                        if (item.Id == id) {
                            this.view.SelectedPatient = item;
                            this.view.PatientLabelValue.Text = item.FullName;

                            this.selectedChart = await this.readPatientChart();
                            await this.readPatientChartDocs(this.selectedChart.Id);
                        }
                    });
                }

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception e) {
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
            }
            catch (Exception e) {
                throw e;
            }
        }

        private async Task<List<Models.ChartDocument>> readPatientChartDocs(int chartId) {
            try {
                List<Models.ChartDocument> docs = await chartDocumentModel.readChartDocuments(chartId);

                this.view.AnalysisTable.DataGrid.Rows.Clear();
                this.view.AnalysisTable.DataGrid.Refresh();

                docs.ForEach((item) => {
                    if (item.Type == ChartDocTypes.ANALIZE) {
                        this.view.AnalysisTable.DataGrid.Rows.Add(
                            item.Id,
                            item.Name,
                            item.URL,
                            item.DateCreated.ToString(DateTimeFormats.SQ_DATE_TIME)
                        );
                    }    
                });

                return docs;
            }
            catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        /**
         * Handle adding docs
         */

        public void handleAddAnalysis() {
            if (this.selectedChart == null) {
                string message = "Nuk është zgjedhur asnjë pacient";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            this.addAnalysisForm = new AddAnalysisForm(this);
            this.addAnalysisForm.Show();
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

                this.addAnalysisForm.Hide();
                await this.readPatientChartDocs(this.selectedChart.Id);

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

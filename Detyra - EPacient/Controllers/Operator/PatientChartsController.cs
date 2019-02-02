using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Operator;

namespace Detyra___EPacient.Controllers.Operator {
    class PatientChartsController {
        private PatientCharts view;

        private Models.Patient patientModel;
        private List<Models.Patient> patients;

        public PatientChartsController(PatientCharts view) {
            this.view = view;
            this.patientModel = new Models.Patient();
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

                    this.patients.ForEach((item) => {
                        if (item.Id == id) {
                            this.view.SelectedPatient = item;
                        }
                    });
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

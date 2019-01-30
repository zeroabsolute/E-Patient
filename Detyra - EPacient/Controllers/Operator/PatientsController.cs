using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Operator;

namespace Detyra___EPacient.Controllers.Operator {
    class PatientsController {
        private Patients view;

        private Models.Patient patientModel;
        private List<Models.Patient> patients;

        public PatientsController(Patients view) {
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

                this.view.CBox.comboBox.DataSource = new List<string> {
                    Genders.MALE,
                    Genders.FEMALE,
                    Genders.OTHER
                };

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
                            this.view.SelectedPatient = $"{item.FirstName} {item.LastName}";
                            this.view.SelectedPatientId = id;
                            this.view.PatientLabelValue.Text = $"{item.FirstName} {item.LastName}";
                            this.view.FirstNameTxtBox.Text = item.FirstName;
                            this.view.LastNameTxtBox.Text = item.LastName;
                            this.view.DateOfBirth.Value = item.DateOfBirth;
                            this.view.PhoneNumberTxtBox.Text = item.PhoneNumber;
                            this.view.AddressTxtBox.Text = item.Address;
                            this.view.CBox.comboBox.SelectedIndex = this.view.CBox.comboBox.FindStringExact(item.Gender);
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
         * Controller to handle reset button
         */
        
        public void handleResetButton() {
            this.view.SelectedPatient = null;
            this.view.SelectedPatientId = -1;
            this.view.PatientLabelValue.Text = "-";
            this.view.FirstNameTxtBox.Text = "";
            this.view.LastNameTxtBox.Text = "";
            this.view.DateOfBirth.Value = DateTime.Now;
            this.view.PhoneNumberTxtBox.Text = "";
            this.view.CBox.comboBox.SelectedIndex = 0;
            this.view.AddressTxtBox.Text = "";
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (this.view.SelectedPatientId < 1) {
                    await this.createPatient(
                        this.view.FirstNameTxtBox.Text,
                        this.view.LastNameTxtBox.Text,
                        this.view.PhoneNumberTxtBox.Text,
                        this.view.AddressTxtBox.Text,
                        this.view.DateOfBirth.Value.ToString(DateTimeFormats.MYSQL_DATE),
                        this.view.CBox.comboBox.SelectedValue.ToString()
                    );
                } else {
                    await this.updatePatient(
                        this.view.SelectedPatientId,
                        this.view.FirstNameTxtBox.Text,
                        this.view.LastNameTxtBox.Text,
                        this.view.PhoneNumberTxtBox.Text,
                        this.view.AddressTxtBox.Text,
                        this.view.DateOfBirth.Value.ToString(DateTimeFormats.MYSQL_DATE),
                        this.view.CBox.comboBox.SelectedValue.ToString()
                    );
                }

                this.handleResetButton();
                this.init();

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<long> createPatient(
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            string dateOfBirth,
            string gender
        ) {
            try {
                long id = await patientModel.createPatient(
                    firstName,
                    lastName,
                    phoneNumber,
                    address,
                    dateOfBirth,
                    gender
                );

                MessageBox.Show("Pacienti u shtua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return id;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return -1;
            }
        }

        private async Task<long> updatePatient(
            long id,
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            string dateOfBirth,
            string gender
        ) {
            try {
                long updatedId = await patientModel.updatePatient(
                    id,
                    firstName,
                    lastName,
                    phoneNumber,
                    address,
                    dateOfBirth,
                    gender
                );
                
                MessageBox.Show("Pacienti u përditësua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return updatedId;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return -1;
            }
        }
    }
}

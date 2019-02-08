using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class EmergencyDoctorsController {
        private DoctorInCharge view;
        private Models.EmergencyDoctor emergencyDoctorModel;
        private Models.Sector sectorModel;

        public EmergencyDoctorsController(DoctorInCharge view) {
            this.view = view;
            this.emergencyDoctorModel = new Models.EmergencyDoctor();
            this.sectorModel = new Models.Sector();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Populate months combobox
                this.view.MonthCBox.comboBox.DisplayMember = "name";
                this.view.MonthCBox.comboBox.ValueMember = "value";
                this.view.MonthCBox.comboBox.DataSource = new List<Models.Month> {
                    Months.JANUARY,
                    Months.FEBRUARY,
                    Months.MARCH,
                    Months.APRIL,
                    Months.MAY,
                    Months.JUNE,
                    Months.JULY,
                    Months.AUGUST,
                    Months.SEPTEMBER,
                    Months.OCTOBER,
                    Months.NOVEMBER,
                    Months.DECEMBER
                };

                // Read sectors from DB and populate combobox
                List<Models.Sector> sectors = await sectorModel.readSectors();

                this.view.SectorsCBox.comboBox.DisplayMember = "name";
                this.view.SectorsCBox.comboBox.ValueMember = "id";
                this.view.SectorsCBox.comboBox.DataSource = sectors;

                Cursor.Current = Cursors.Arrow;

                // Read data from DB and populate table
                List<Models.EmergencyDoctor> data = await this.emergencyDoctorModel.readEmergencyDoctors(
                    (int) this.view.SectorsCBox.comboBox.SelectedValue,
                    this.view.MonthCBox.comboBox.SelectedValue.ToString(),
                    DateTime.Now.Year.ToString()
                );

                this.populateTable(data);

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (
                    this.view.SectorsCBox.comboBox.SelectedValue != null
                        && this.view.MonthCBox.comboBox.SelectedValue != null
                        && this.view.YearTxtBox.Text.Length == 4
                ) {
                    await this.emergencyDoctorModel.generateEmergencyDoctors(
                        (int) this.view.SectorsCBox.comboBox.SelectedValue,
                        this.view.MonthCBox.comboBox.SelectedValue.ToString(),
                        this.view.YearTxtBox.Text
                    );

                    List<Models.EmergencyDoctor> data = await emergencyDoctorModel.readEmergencyDoctors(
                        (int) this.view.SectorsCBox.comboBox.SelectedValue,
                        this.view.MonthCBox.comboBox.SelectedValue.ToString(),
                        this.view.YearTxtBox.Text
                    );

                    this.populateTable(data);
                }

                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("Mjekët roje u gjeneruan me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Helper to populate table
         */

        private void populateTable(List<Models.EmergencyDoctor> data) {
            this.view.EmergencyDoctorsTable.DataGrid.Rows.Clear();
            this.view.EmergencyDoctorsTable.DataGrid.Refresh();

            data.ForEach((item) => {
                this.view.EmergencyDoctorsTable.DataGrid.Rows.Add(
                    item.Id,
                    item.Doctor.Employee.FirstName,
                    item.Doctor.Employee.LastName,
                    item.Date.ToString(DateTimeFormats.SQ_DATE)
                );
            });
        }

        /**
         * Handle filter change
         */

        public async void handleFilterChange() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (
                    this.view.SectorsCBox.comboBox.SelectedValue != null
                        && this.view.MonthCBox.comboBox.SelectedValue != null
                        && this.view.YearTxtBox.Text.Length == 4
                ) {
                    List<Models.EmergencyDoctor> data = await emergencyDoctorModel.readEmergencyDoctors(
                        (int) this.view.SectorsCBox.comboBox.SelectedValue,
                        this.view.MonthCBox.comboBox.SelectedValue.ToString(),
                        this.view.YearTxtBox.Text
                    );

                    this.populateTable(data);
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

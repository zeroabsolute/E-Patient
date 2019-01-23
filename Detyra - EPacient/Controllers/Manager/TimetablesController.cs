using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class TimetablesController {
        private Timetables view;
        
        private Models.Employee employeeModel;
        private Models.WorkingHours workingHoursModel;

        public TimetablesController(Timetables view) {
            this.view = view;
            this.employeeModel = new Models.Employee();
            this.workingHoursModel = new Models.WorkingHours();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read employees from DB and populate table
                List<Models.Employee> roles = await this.employeeModel.readEmployees();

                this.view.EmployeesTable.DataGrid.Rows.Clear();
                this.view.EmployeesTable.DataGrid.Refresh();

                roles.ForEach((item) => {
                    this.view.EmployeesTable.DataGrid.Rows.Add(
                        item.Id,
                        item.User.Email,
                        item.FirstName,
                        item.LastName
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

        public async void handleTableRowSelection() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                var selectedRow = this.view.EmployeesTable.DataGrid.SelectedRows.Count > 0
                ? this.view.EmployeesTable.DataGrid.SelectedRows[0]
                : null;

                if (selectedRow != null) {
                    int id = (int) selectedRow.Cells[0].Value;
                    string firstName = selectedRow.Cells[2].Value.ToString();
                    string lastName = selectedRow.Cells[3].Value.ToString();

                    this.view.SelectedEmployee = $"{firstName} {lastName}";
                    this.view.SelectedEmployeeId = id;
                    this.view.EmployeeLabelValue.Text = this.view.SelectedEmployee;

                    // Read working hours (if there are any existing)
                    Models.WorkingHours workingHours = await workingHoursModel.readWorkingHours(id);

                    this.view.SelectedWorkingHoursId = workingHours != null ? workingHours.Id : -1;
                    this.view.MondayTimePickerStart.Value = workingHours != null ? workingHours.MondayStartTime : DateTime.Now.Date;
                    this.view.MondayTimePickerEnd.Value = workingHours != null ? workingHours.MondayEndTime : DateTime.Now.Date;
                    this.view.TuesdayTimePickerStart.Value = workingHours != null ? workingHours.TuesdayStartTime : DateTime.Now.Date;
                    this.view.TuesdayTimePickerEnd.Value = workingHours != null ? workingHours.TuesdayEndTime : DateTime.Now.Date;
                    this.view.WednesdayTimePickerStart.Value = workingHours != null ? workingHours.WednesdayStartTime : DateTime.Now.Date;
                    this.view.WednesdayTimePickerEnd.Value = workingHours != null ? workingHours.WednesdayEndTime : DateTime.Now.Date;
                    this.view.ThursdayTimePickerStart.Value = workingHours != null ? workingHours.ThursdayStartTime : DateTime.Now.Date;
                    this.view.ThursdayTimePickerEnd.Value = workingHours != null ? workingHours.ThursdayEndTime : DateTime.Now.Date;
                    this.view.FridayTimePickerStart.Value = workingHours != null ? workingHours.FridayStartTime : DateTime.Now.Date;
                    this.view.FridayTimePickerEnd.Value = workingHours != null ? workingHours.FridayEndTime : DateTime.Now.Date;
                    this.view.SaturdayTimePickerStart.Value = workingHours != null ? workingHours.SaturdayStartTime : DateTime.Now.Date;
                    this.view.SaturdayTimePickerEnd.Value = workingHours != null ? workingHours.SaturdayEndTime : DateTime.Now.Date;
                    this.view.SundayTimePickerStart.Value = workingHours != null ? workingHours.SundayStartTime : DateTime.Now.Date;
                    this.view.SundayTimePickerEnd.Value = workingHours != null ? workingHours.SundayEndTime : DateTime.Now.Date;
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
            this.view.SelectedEmployee = null;
            this.view.SelectedEmployeeId = -1;
            this.view.SelectedWorkingHoursId = -1;
            this.view.EmployeeLabelValue.Text = "-";
            
            this.view.MondayTimePickerStart.Value = DateTime.Now.Date;
            this.view.MondayTimePickerEnd.Value = DateTime.Now.Date;
            this.view.TuesdayTimePickerStart.Value = DateTime.Now.Date;
            this.view.TuesdayTimePickerEnd.Value = DateTime.Now.Date;
            this.view.WednesdayTimePickerStart.Value = DateTime.Now.Date;
            this.view.WednesdayTimePickerEnd.Value = DateTime.Now.Date;
            this.view.ThursdayTimePickerStart.Value =  DateTime.Now.Date;
            this.view.ThursdayTimePickerEnd.Value = DateTime.Now.Date;
            this.view.FridayTimePickerStart.Value = DateTime.Now.Date;
            this.view.FridayTimePickerEnd.Value = DateTime.Now.Date;
            this.view.SaturdayTimePickerStart.Value = DateTime.Now.Date;
            this.view.SaturdayTimePickerEnd.Value = DateTime.Now.Date;
            this.view.SundayTimePickerStart.Value = DateTime.Now.Date;
            this.view.SundayTimePickerEnd.Value = DateTime.Now.Date;
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                long id = await workingHoursModel.updateWorkingHours(
                    this.view.SelectedWorkingHoursId,
                    this.view.SelectedEmployeeId,
                    this.view.MondayTimePickerStart.Value,
                    this.view.MondayTimePickerEnd.Value,
                    this.view.TuesdayTimePickerStart.Value,
                    this.view.TuesdayTimePickerEnd.Value,
                    this.view.WednesdayTimePickerStart.Value,
                    this.view.WednesdayTimePickerEnd.Value,
                    this.view.ThursdayTimePickerStart.Value,
                    this.view.ThursdayTimePickerEnd.Value,
                    this.view.FridayTimePickerStart.Value,
                    this.view.FridayTimePickerEnd.Value,
                    this.view.SaturdayTimePickerStart.Value,
                    this.view.SaturdayTimePickerEnd.Value,
                    this.view.SundayTimePickerStart.Value,
                    this.view.SundayTimePickerEnd.Value
                );

                this.view.SelectedWorkingHoursId = id;

                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("Oraret e punës u përditësuan me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.handleResetButton();
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

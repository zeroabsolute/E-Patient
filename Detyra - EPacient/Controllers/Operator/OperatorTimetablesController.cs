using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Operator;

namespace Detyra___EPacient.Controllers.Operator {
    class OperatorTimetablesController {
        private OperatorTimetables view;
        
        private Models.Employee employeeModel;
        private Models.WorkingHours workingHoursModel;

        public OperatorTimetablesController(OperatorTimetables view) {
            this.view = view;
            this.employeeModel = new Models.Employee();
            this.workingHoursModel = new Models.WorkingHours();
        }

        /**
         * Helper to populate table with data
         */

        private void populateTable(List<Models.Employee> data) {
            this.view.EmployeesTable.DataGrid.Rows.Clear();
            this.view.EmployeesTable.DataGrid.Refresh();

            data.ForEach((item) => {
                this.view.EmployeesTable.DataGrid.Rows.Add(
                    item.Id,
                    item.User.Email,
                    item.FirstName,
                    item.LastName
                );
            });
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read employees from DB and populate table
                List<Models.Employee> employees = await this.employeeModel.readEmployees();
                this.view.Employees = employees;

                this.populateTable(employees);

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
                    this.view.MondayStartLabel.Text = workingHours != null ? workingHours.MondayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.MondayEndLabel.Text = workingHours != null ? workingHours.MondayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.TuesdayStartLabel.Text = workingHours != null ? workingHours.TuesdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.TuesdayEndLabel.Text = workingHours != null ? workingHours.TuesdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.WednesdayStartLabel.Text = workingHours != null ? workingHours.WednesdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.WednesdayEndLabel.Text = workingHours != null ? workingHours.WednesdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.ThursdayStartLabel.Text = workingHours != null ? workingHours.ThursdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.ThursdayEndLabel.Text = workingHours != null ? workingHours.ThursdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.FridayStartLabel.Text = workingHours != null ? workingHours.MondayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.FridayEndLabel.Text = workingHours != null ? workingHours.MondayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.SaturdayStartLabel.Text = workingHours != null ? workingHours.SaturdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.SaturdayEndLabel.Text = workingHours != null ? workingHours.SaturdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.SundayStartLabel.Text = workingHours != null ? workingHours.SundayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                    this.view.SundayEndLabel.Text = workingHours != null ? workingHours.SundayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle search by name
         */

        public void handleSearch() {
            string searchTerm = this.view.SearchTermTxtBox.Text;
            List<Models.Employee> filteredEmployees = new List<Models.Employee>();

            if (searchTerm.Length == 0) {
                this.populateTable(this.view.Employees);

                return;
            }

            if (this.view.Employees != null && this.view.Employees.Count > 0) {
                this.view.Employees.ForEach((item) => {
                    string fullName = $"{item.FirstName} {item.LastName}";

                    if (fullName.StartsWith(searchTerm)) {
                        filteredEmployees.Add(item);
                    }
                });

                this.populateTable(filteredEmployees);
            }
        }
    }
}

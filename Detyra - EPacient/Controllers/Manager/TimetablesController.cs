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

        public TimetablesController(Timetables view) {
            this.view = view;
            this.employeeModel = new Models.Employee();
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
            }
        }
    }
}

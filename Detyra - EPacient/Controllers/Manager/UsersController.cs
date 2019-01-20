using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class UsersController {
        private Users view;

        private Models.Role roleModel;
        private Models.Operator operatorModel;
        private Models.Doctor doctorModel;
        private Models.Nurse nurseModel;

        public UsersController(Users view) {
            this.view = view;
            this.roleModel = new Models.Role();
            this.operatorModel = new Models.Operator();
            this.doctorModel = new Models.Doctor();
            this.nurseModel = new Models.Nurse();
        }

        /**
         * Controller to handle initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read roles from DB and populate combobox
                List<Models.Role> roles = await roleModel.readRoles();
                List<Models.Role> filteredRoles = new List<Models.Role>();

                roles.ForEach((role) => {
                    if (role.Name != Roles.MANAGER) {
                        filteredRoles.Add(role);
                    }
                });

                this.view.CBox.comboBox.DisplayMember = "name";
                this.view.CBox.comboBox.ValueMember = "id";
                this.view.CBox.comboBox.DataSource = filteredRoles;

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle role selection via combobox
         */

        public async void handleRoleSelection(object sender) {
            try {
                Cursor.Current = Cursors.WaitCursor;

                ComboBox comboBox = (ComboBox) sender;
                Models.Role selectedItem = (Models.Role) comboBox.SelectedItem;

                switch (selectedItem.Name) {
                    case Roles.OPERATOR:
                        List<Models.Operator> operators = await operatorModel.readOperators();
                        this.view.Operators = operators;
                        this.view.SelectedRole = Roles.OPERATOR;
                        break;
                    case Roles.DOCTOR:
                        List<Models.Doctor> doctors = await doctorModel.readDoctors();
                        this.view.Doctors = doctors;
                        this.view.SelectedRole = Roles.DOCTOR;
                        break;
                    case Roles.NURSE:
                        List<Models.Nurse> nurses = await nurseModel.readNurses();
                        this.view.Nurses = nurses;
                        this.view.SelectedRole = Roles.NURSE;
                        break;
                    default:
                        break;
                }

                this.populateUsersTable();

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to populate table with user data.
         * Output will be dependent on the selected role.
         */

        private void populateUsersTable() {
            this.view.UsersTable.DataGrid.Rows.Clear();
            this.view.UsersTable.DataGrid.Refresh();

            if (this.view.SelectedRole == Roles.OPERATOR) {
                this.view.Operators.ForEach((item) => {
                    this.view.UsersTable.DataGrid.Rows.Add(
                        item.Id,
                        item.User.Email,
                        item.FirstName,
                        item.LastName
                    );
                });
            } else if (this.view.SelectedRole == Roles.DOCTOR) {
                this.view.Doctors.ForEach((item) => {
                    this.view.UsersTable.DataGrid.Rows.Add(
                        item.Id,
                        item.Employee.User.Email,
                        item.Employee.FirstName,
                        item.Employee.LastName
                    );
                });
            } else if (this.view.SelectedRole == Roles.NURSE) {
                this.view.Nurses.ForEach((item) => {
                    this.view.UsersTable.DataGrid.Rows.Add(
                        item.Id,
                        item.Employee.User.Email,
                        item.Employee.FirstName,
                        item.Employee.LastName
                    );
                });
            }
        }
    }
}

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
        private Models.User userModel;
        private Models.Employee employeeModel;
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

        public async void handleRoleSelection() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                Models.Role selectedItem = (Models.Role) this.view.CBox.comboBox.SelectedItem;

                switch (selectedItem.Name) {
                    case Roles.OPERATOR:
                        List<Models.Operator> operators = await operatorModel.readOperators();
                        this.view.Operators = operators;
                        this.view.SelectedRole = Roles.OPERATOR;
                        this.view.FormRoleLabel.Text = Roles.OPERATOR;
                        this.view.FormAddressLabel.Visible = false;
                        this.view.FormAddressTxtBox.Visible = false;
                        this.view.FormPhoneNumberLabel.Visible = false;
                        this.view.FormPhoneNumberTxtBox.Visible = false;
                        this.view.FormSpecializationLabel.Visible = false;
                        this.view.FormSpecializationTxtBox.Visible = false;
                        break;
                    case Roles.DOCTOR:
                        List<Models.Doctor> doctors = await doctorModel.readDoctors();
                        this.view.Doctors = doctors;
                        this.view.SelectedRole = Roles.DOCTOR;
                        this.view.FormRoleLabel.Text = Roles.DOCTOR;
                        this.view.FormAddressLabel.Visible = true;
                        this.view.FormAddressTxtBox.Visible = true;
                        this.view.FormPhoneNumberLabel.Visible = true;
                        this.view.FormPhoneNumberTxtBox.Visible = true;
                        this.view.FormSpecializationLabel.Visible = true;
                        this.view.FormSpecializationTxtBox.Visible = true;
                        break;
                    case Roles.NURSE:
                        List<Models.Nurse> nurses = await nurseModel.readNurses();
                        this.view.Nurses = nurses;
                        this.view.SelectedRole = Roles.NURSE;
                        this.view.FormRoleLabel.Text = Roles.NURSE;
                        this.view.FormAddressLabel.Visible = true;
                        this.view.FormAddressTxtBox.Visible = true;
                        this.view.FormPhoneNumberLabel.Visible = true;
                        this.view.FormPhoneNumberTxtBox.Visible = true;
                        this.view.FormSpecializationLabel.Visible = false;
                        this.view.FormSpecializationTxtBox.Visible = false;
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
            try {
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
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to clear form input
         */

        public void handleClear() {
            try {
                this.view.FormEmailTxtBox.Text = "";
                this.view.FormPasswordTxtBox.Text = "";
                this.view.FormFirstNameTxtBox.Text = "";
                this.view.FormLastNameTxtBox.Text = "";
                this.view.FormDOBPicker.Text = "";
                this.view.FormPhoneNumberTxtBox.Text = "";
                this.view.FormAddressTxtBox.Text = "";
                this.view.FormAddressTxtBox.Text = "";
                this.view.FormSpecializationTxtBox.Text = "";
            } catch (Exception e) {
                string caption = "Problem në fshirje";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to submit user information
         */

        public async void handleSubmit() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                int role = (int) this.view.CBox.comboBox.SelectedValue;
                string email = this.view.FormEmailTxtBox.Text;
                string password = this.view.FormPasswordTxtBox.Text;
                string firstName = this.view.FormFirstNameTxtBox.Text;
                string lastName = this.view.FormLastNameTxtBox.Text;
                string dob = this.view.FormDOBPicker.Value.ToString("yyyy-MM-dd");
                string phoneNumber = this.view.FormPhoneNumberTxtBox.Text;
                string address = this.view.FormAddressTxtBox.Text;
                string specialization = this.view.FormSpecializationTxtBox.Text;

                // Create user
                userModel = new Models.User();
                long createdUserId = await userModel.createUser(email, password, role);

                // Create objects based on selected role
                if (this.view.SelectedRole == Roles.OPERATOR) {
                    operatorModel = new Models.Operator();
                    long createdOperatorId = await operatorModel.createOperator(
                        firstName,
                        lastName,
                        dob,
                        createdUserId
                    );
                } else if (this.view.SelectedRole == Roles.DOCTOR) {
                    employeeModel = new Models.Employee();
                    long createdEmployeeId = await employeeModel.createEmployee(
                        firstName,
                        lastName,
                        phoneNumber,
                        address,
                        dob,
                        createdUserId
                    );

                    doctorModel = new Models.Doctor();
                    await doctorModel.createDoctor(specialization, createdEmployeeId);
                } else if (this.view.SelectedRole == Roles.NURSE) {
                    employeeModel = new Models.Employee();
                    long createdEmployeeId = await employeeModel.createEmployee(
                        firstName,
                        lastName,
                        phoneNumber,
                        address,
                        dob,
                        createdUserId
                    );

                    nurseModel = new Models.Nurse();
                    await nurseModel.createNurse(createdEmployeeId);
                }

                this.handleRoleSelection();
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("Përdoruesi u shtua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

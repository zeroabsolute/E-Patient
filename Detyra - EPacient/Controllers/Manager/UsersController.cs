using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class UsersController {
        private Users view;
        private int selectedUserId;
        private int selectedUserStatus;

        private Models.Role roleModel;
        private Models.Sector sectorModel;
        private Models.User userModel;
        private Models.Employee employeeModel;
        private Models.Operator operatorModel;
        private Models.Doctor doctorModel;
        private Models.Nurse nurseModel;

        public UsersController(Users view) {
            this.view = view;
            this.userModel = new Models.User();
            this.roleModel = new Models.Role();
            this.sectorModel = new Models.Sector();
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

                roles.Add(new Models.Role(
                    -1,
                    Roles.ALL
                 ));

                roles.ForEach((role) => {
                    if (role.Name != Roles.MANAGER) {
                        filteredRoles.Add(role);
                    }
                });

                this.view.CBox.comboBox.DisplayMember = "name";
                this.view.CBox.comboBox.ValueMember = "id";
                this.view.CBox.comboBox.DataSource = filteredRoles;

                // Read sectors from DB and populate combobox
                List<Models.Sector> sectors = await sectorModel.readSectors();

                this.view.SpecializationCBox.comboBox.DisplayMember = "name";
                this.view.SpecializationCBox.comboBox.ValueMember = "id";
                this.view.SpecializationCBox.comboBox.DataSource = sectors;

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
                        this.view.SpecializationCBox.comboBox.Visible = false;
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
                        this.view.SpecializationCBox.comboBox.Visible = true;
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
                        this.view.SpecializationCBox.comboBox.Visible = false;
                        break;
                    case Roles.ALL:
                        List<Models.Operator> o = await operatorModel.readOperators();
                        this.view.Operators = o;
                        List<Models.Doctor> d = await doctorModel.readDoctors();
                        this.view.Doctors = d;
                        List<Models.Nurse> n = await nurseModel.readNurses();
                        this.view.Nurses = n;
                        this.view.SelectedRole = Roles.ALL;
                        this.view.FormRoleLabel.Text = Roles.ALL;
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
                            item.User.Id,
                            item.User.Email,
                            item.FirstName,
                            item.LastName,
                            item.User.Status == Statuses.ACTIVE.Id ? Statuses.ACTIVE.Name : Statuses.INACTIVE.Name
                        );
                    });
                } else if (this.view.SelectedRole == Roles.DOCTOR) {
                    this.view.Doctors.ForEach((item) => {
                        this.view.UsersTable.DataGrid.Rows.Add(
                            item.Employee.User.Id,
                            item.Employee.User.Email,
                            item.Employee.FirstName,
                            item.Employee.LastName,
                            item.Employee.User.Status == Statuses.ACTIVE.Id ? Statuses.ACTIVE.Name : Statuses.INACTIVE.Name
                        );
                    });
                } else if (this.view.SelectedRole == Roles.NURSE) {
                    this.view.Nurses.ForEach((item) => {
                        this.view.UsersTable.DataGrid.Rows.Add(
                            item.Employee.User.Id,
                            item.Employee.User.Email,
                            item.Employee.FirstName,
                            item.Employee.LastName,
                            item.Employee.User.Status == Statuses.ACTIVE.Id ? Statuses.ACTIVE.Name : Statuses.INACTIVE.Name
                        );
                    });
                } else if (this.view.SelectedRole == Roles.ALL) {
                    this.view.Operators.ForEach((item) => {
                        this.view.UsersTable.DataGrid.Rows.Add(
                            item.User.Id,
                            item.User.Email,
                            item.FirstName,
                            item.LastName,
                            item.User.Status == Statuses.ACTIVE.Id ? Statuses.ACTIVE.Name : Statuses.INACTIVE.Name
                        );
                    });
                    this.view.Doctors.ForEach((item) => {
                        this.view.UsersTable.DataGrid.Rows.Add(
                            item.Employee.User.Id,
                            item.Employee.User.Email,
                            item.Employee.FirstName,
                            item.Employee.LastName,
                            item.Employee.User.Status == Statuses.ACTIVE.Id ? Statuses.ACTIVE.Name : Statuses.INACTIVE.Name
                        );
                    });
                    this.view.Nurses.ForEach((item) => {
                        this.view.UsersTable.DataGrid.Rows.Add(
                            item.Employee.User.Id,
                            item.Employee.User.Email,
                            item.Employee.FirstName,
                            item.Employee.LastName,
                            item.Employee.User.Status == Statuses.ACTIVE.Id ? Statuses.ACTIVE.Name : Statuses.INACTIVE.Name
                        );
                    });
                }
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

                var selectedRow = this.view.UsersTable.DataGrid.SelectedRows.Count > 0
                ? this.view.UsersTable.DataGrid.SelectedRows[0]
                : null;

                if (selectedRow != null) {
                    this.selectedUserId = (int) selectedRow.Cells[0].Value;
                    string status = selectedRow.Cells[4].Value.ToString();
                    this.selectedUserStatus = status == Statuses.ACTIVE.Name ? Statuses.ACTIVE.Id : Statuses.INACTIVE.Id;

                    this.modifyButton(this.selectedUserStatus);
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Modify button according to status
         */

        private void modifyButton(int status) {
            string text = status == Statuses.ACTIVE.Id ? "ÇAKTIVIZO" : "AKTIVIZO";
            Color color = status == Statuses.ACTIVE.Id ? Colors.IMPERIAL_RED : Colors.BLUE_LAGOON;
            Image image = status == Statuses.ACTIVE.Id
                ? Image.FromFile("../../Resources/delete.png")
                : Image.FromFile("../../Resources/restore.png");

            this.view.DeleteBtn.BackColor = color;
            this.view.DeleteBtn.Text = text;
            this.view.DeleteBtn.Image = image;
        }

        /**
         * Controller to deactivate a user
         */

        public async void handleDeactivate() {
            try {
                if (this.selectedUserId != -1) {
                    Models.Status newStatus = await userModel.toggleArchive(this.selectedUserId, this.selectedUserStatus);
                    
                    if (this.selectedUserId > 0) {
                        this.modifyButton(newStatus.Id);
                        this.handleRoleSelection();
                    }
                }
            } catch (Exception e) {
                string caption = "Problem në fshirje";
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
                string dob = this.view.FormDOBPicker.Value.ToString(DateTimeFormats.MYSQL_DATE);
                string phoneNumber = this.view.FormPhoneNumberTxtBox.Text;
                string address = this.view.FormAddressTxtBox.Text;
                int specialization = (int) this.view.SpecializationCBox.comboBox.SelectedValue;

                // Check if one role is selected
                if (role < 1) {
                    MessageBox.Show("Nuk është zgjedhur roli!", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

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

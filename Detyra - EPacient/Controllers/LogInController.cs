using System;
using System.Windows.Forms;

using Detyra___EPacient.Models;
using Detyra___EPacient.Views;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Controllers {
    class LogInController {
        private LogInPanel view;

        public LogInController(LogInPanel view) {
            this.view = view;
        }

        public async void handleLogIn() {
            try {
                User user = new User();
                string email = this.view.EmailTextBox.Text;
                string password = this.view.PasswordTxtBox.Text;

                await user.userLogIn(email, password);

                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine(user.toString());
                Console.WriteLine("--------------------------------------------------------------------");

                // Based on user role, decide where to go next
                switch (user.Role.Name) {
                    case Roles.MANAGER:
                        this.view.ManagerMainPanel.LoggedInUser = user;
                        Panels.switchPanels(this.view.Panel, this.view.ManagerMainPanel.Panel);
                        break;
                    case Roles.OPERATOR:
                        this.view.OperatorMainPanel.LoggedInUser = user;
                        Panels.switchPanels(this.view.Panel, this.view.OperatorMainPanel.Panel);
                        break;
                    case Roles.DOCTOR:
                        this.view.DoctorMainPanel.LoggedInUser = user;
                        Panels.switchPanels(this.view.Panel, this.view.DoctorMainPanel.Panel);
                        break;
                    case Roles.NURSE:
                        this.view.NurseMainPanel.LoggedInUser = user;
                        Panels.switchPanels(this.view.Panel, this.view.NurseMainPanel.Panel);
                        break;
                    default:
                        break;
                }
            } catch (Exception e) {
                string caption = "Problem në identifikim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

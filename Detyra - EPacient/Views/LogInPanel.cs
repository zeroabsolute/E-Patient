using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Controllers;
using Detyra___EPacient.Models;

namespace Detyra___EPacient.Views {
    class LogInPanel {
        // UI Form components
        private Panel operatorMainPanel = null;
        private Panel panel = null;
        private TableLayoutPanel formContainer = null;
        private Label headerLabel = null;
        private Label emailLabel = null;
        private TextBox emailTxtBox = null;
        private Label passwordLabel = null;
        private TextBox passwordTxtBox = null;
        private Button logInBtn = null;

        // Dimensions
        private int formContainerHeight = Dimensions.VIEW_HEIGHT - 300;
        private int formContainerWidth = (int) Dimensions.VIEW_WIDTH / 3;
        private int formContainerPadding = 20;
        private int formComponentWidth = 0;
        private int labelHeight = 20;

        // Variables
        LogInController controller;

        /* Constructor */

        public LogInPanel() {
            controller = new LogInController();

            // Init dimensions
            this.formComponentWidth = this.formContainerWidth - 2 * this.formContainerPadding;
            Padding headerLabelMargins = new Padding(this.formContainerPadding);
            Padding txtBoxMargins = new Padding(this.formContainerPadding, this.formContainerPadding, 0, 0);
            Padding labelMargins = new Padding(this.formContainerPadding, this.formContainerPadding, 0, 0);

            // Init panel
            this.panel = new Panel();
            this.panel.AutoSize = true;
            this.panel.Location = new Point(0, 0);
            this.panel.Name = "logInPanel";
            this.panel.Size = new Size(Dimensions.VIEW_WIDTH, Dimensions.VIEW_HEIGHT);
            this.panel.BackColor = Colors.ALTO;

            // Init log in form container
            int formContainerX = Panels.getComponentStartingPositionX(Dimensions.VIEW_WIDTH, formContainerWidth);
            int formContainerY = Panels.getComponentStartingPositionY(Dimensions.VIEW_HEIGHT, formContainerHeight);

            this.formContainer = new TableLayoutPanel();
            this.formContainer.Location = new Point(formContainerX, formContainerY);
            this.formContainer.Name = "formContainer";
            this.formContainer.Size = new Size(formContainerWidth, formContainerHeight);
            this.formContainer.BackColor = Colors.WHITE;

            this.panel.Controls.Add(this.formContainer);

            // Init header label
            this.headerLabel = new Label();
            this.headerLabel.Width = this.formComponentWidth;
            this.headerLabel.Height = 60;
            this.headerLabel.Text = "Identifikimi";
            this.headerLabel.Font = new Font(Fonts.primary, 32, FontStyle.Bold);
            this.headerLabel.ForeColor = Colors.MALACHITE;
            this.headerLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.headerLabel.Margin = headerLabelMargins;

            this.formContainer.Controls.Add(headerLabel, 0, 0);

            // Init email label
            this.emailLabel = new Label();
            this.emailLabel.Width = this.formComponentWidth;
            this.emailLabel.Height = this.labelHeight;
            this.emailLabel.Text = "Email";
            this.emailLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.emailLabel.ForeColor = Colors.DOVE_GRAY;
            this.emailLabel.TextAlign = ContentAlignment.BottomLeft;
            this.emailLabel.Margin = labelMargins;

            this.formContainer.Controls.Add(emailLabel, 0, 1);

            // Init email text box
            this.emailTxtBox = new TextBox();
            this.emailTxtBox.Width = this.formComponentWidth;
            this.emailTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.emailTxtBox.Margin = txtBoxMargins;

            this.formContainer.Controls.Add(emailTxtBox, 0, 2);

            // Init password label
            this.passwordLabel = new Label();
            this.passwordLabel.Width = this.formComponentWidth;
            this.passwordLabel.Height = this.labelHeight;
            this.passwordLabel.Text = "Fjalëkalimi";
            this.passwordLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.passwordLabel.ForeColor = Colors.DOVE_GRAY;
            this.passwordLabel.TextAlign = ContentAlignment.BottomLeft;
            this.passwordLabel.Margin = labelMargins;

            this.formContainer.Controls.Add(passwordLabel, 0, 3);

            // Init password text box
            Padding passwordTxtBoxMargins = new Padding(this.formContainerPadding, this.formContainerPadding, 0, 50);

            this.passwordTxtBox = new TextBox();
            this.passwordTxtBox.PasswordChar = '*';
            this.passwordTxtBox.Width = this.formComponentWidth;
            this.passwordTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.passwordTxtBox.Margin = passwordTxtBoxMargins;

            this.formContainer.Controls.Add(passwordTxtBox, 0, 4);

            // Init log in button
            Padding logInButtonMargins = new Padding(this.formContainerPadding, this.formContainerPadding, 100, 0);

            this.logInBtn = new Button();
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new Size(formComponentWidth, 60);
            this.logInBtn.Text = "HYR";
            this.logInBtn.UseVisualStyleBackColor = true;
            this.logInBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.logInBtn.ForeColor = Colors.WHITE;
            this.logInBtn.BackColor = Colors.MALACHITE;
            this.logInBtn.FlatStyle = FlatStyle.Flat;
            this.logInBtn.Margin = logInButtonMargins;
            this.logInBtn.Click += new EventHandler(onLogInBtnClicked);

            this.formContainer.Controls.Add(this.logInBtn, 0, 6);
        }

        /* Setters */

        public void initNextPanels(Panel operatorMainPanel)
        {
            if (operatorMainPanel != null)
            {
                this.operatorMainPanel = operatorMainPanel;
            }
        }

        /* Getters */

        public Panel getPanel() {
            return this.panel;
        }

        /* Event handlers */

        private void onLogInBtnClicked(object sender, EventArgs e) {
            string email = this.emailTxtBox.Text;
            string password = this.passwordTxtBox.Text;

            try {
                User user = controller.logIn(email, password);

                // Based on user role, decide where to go next
                switch (user.getRole()) {
                    case Roles.OPERATOR:
                        Panels.switchPanels(this.panel, this.operatorMainPanel);
                        break;
                    default:
                        break;
                }
            } catch (Exception ex) {
                string caption = "Problem në identifikim";
                MessageBox.Show(ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } 
}

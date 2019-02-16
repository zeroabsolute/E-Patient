using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Controllers;
using Detyra___EPacient.Views.Doctor;
using Detyra___EPacient.Views.Operator;
using Detyra___EPacient.Views.Manager;
using Detyra___EPacient.Views.Nurse;

namespace Detyra___EPacient.Views {
    class LogInPanel {
        // UI Form components
        public Panel Panel { get; set; }
        public ManagerMainPanel ManagerMainPanel { get; set; }
        public OperatorMainPanel OperatorMainPanel { get; set; }
        public DoctorMainPanel DoctorMainPanel { get; set; }
        public NurseMainPanel NurseMainPanel { get; set; }
        public TextBox EmailTextBox { get; set; }
        public TextBox PasswordTxtBox { get; set; }

        private TableLayoutPanel formContainer = null;
        private Label headerLabel = null;
        private Label emailLabel = null;
        private Label passwordLabel = null;
        private Button logInBtn = null;

        // Dimensions
        private int formContainerHeight = Dimensions.VIEW_HEIGHT - 300;
        private int formContainerWidth = (int) Dimensions.PANEL_WIDTH / 3;
        private int formContainerPadding = 20;
        private int formComponentWidth = 0;
        private int labelHeight = 20;

        // Variables
        LogInController controller;

        /* Constructor */

        public LogInPanel() {
            controller = new LogInController(this);

            // Init dimensions
            this.formComponentWidth = this.formContainerWidth - 2 * this.formContainerPadding;
            Padding headerLabelMargins = new Padding(this.formContainerPadding);
            Padding txtBoxMargins = new Padding(this.formContainerPadding, this.formContainerPadding, 0, 0);
            Padding labelMargins = new Padding(this.formContainerPadding, this.formContainerPadding, 0, 0);

            // Init Panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "logInPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.BackColor = Colors.ALTO;
            this.Panel.Visible = true;

            // Init log in form container
            int formContainerX = Panels.getComponentStartingPositionX(Dimensions.PANEL_WIDTH, formContainerWidth);
            int formContainerY = Panels.getComponentStartingPositionY(Dimensions.PANEL_HEIGHT, formContainerHeight);

            this.formContainer = new TableLayoutPanel();
            this.formContainer.Location = new Point(formContainerX, formContainerY);
            this.formContainer.Name = "formContainer";
            this.formContainer.Size = new Size(formContainerWidth, formContainerHeight);
            this.formContainer.BackColor = Colors.WHITE;

            this.Panel.Controls.Add(this.formContainer);

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
            this.EmailTextBox = new TextBox();
            this.EmailTextBox.Width = this.formComponentWidth;
            this.EmailTextBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.EmailTextBox.Margin = txtBoxMargins;

            this.formContainer.Controls.Add(EmailTextBox, 0, 2);

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

            this.PasswordTxtBox = new TextBox();
            this.PasswordTxtBox.PasswordChar = '*';
            this.PasswordTxtBox.Width = this.formComponentWidth;
            this.PasswordTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.PasswordTxtBox.Margin = passwordTxtBoxMargins;

            this.formContainer.Controls.Add(PasswordTxtBox, 0, 4);

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
            this.logInBtn.Image = Image.FromFile("../../Resources/done.png");
            this.logInBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.logInBtn.Click += new EventHandler(onLogInBtnClicked);

            this.formContainer.Controls.Add(this.logInBtn, 0, 6);
        }


        /* Setters */

        public void initNextPanels(ManagerMainPanel m, OperatorMainPanel o, DoctorMainPanel d, NurseMainPanel n) {
            if (m != null) {
                this.ManagerMainPanel = m;
            }
            if (o != null) {
                this.OperatorMainPanel = o;
            }
            if (d != null) {
                this.DoctorMainPanel = d;
            }
            if (n != null) {
                this.NurseMainPanel = n;
            }
        }


        /* Event handlers */

        private void onLogInBtnClicked(object sender, EventArgs e) {
            controller.handleLogIn();
        }
    } 
}

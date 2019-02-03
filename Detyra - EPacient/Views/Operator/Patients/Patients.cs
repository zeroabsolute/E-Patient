using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Operator;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Operator {
    class Patients {
        public long SelectedPatientId { get; set; }
        public string SelectedPatient { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Patient> PatientsList { get; set; }
        public DynamicPatientsTable PatientsTable { get; set; }
        public Label PatientLabelValue { get; set; }
        public TextBox FirstNameTxtBox { get; set; }
        public TextBox LastNameTxtBox { get; set; }
        public DateTimePicker DateOfBirth { get; set; }
        public TextBox PhoneNumberTxtBox { get; set; }
        public TextBox AddressTxtBox { get; set; }
        public DynamicComboBox CBox { get; set; }

        private PatientsController controller;
        private NavigationBar header;
        private Point tableLocation;
        private Size tableSize;
        private GroupBox right;
        private Label patientLabel;
        private Label firstNameLabel;
        private Label lastNameLabel;
        private Label genderLabel;
        private Label dateOfBirthLabel;
        private Label phoneNumberLabel;
        private Label addressLabel;
        private Button submitBtn;
        private Button resetBtn;

        private int tableWidth;
        private int tableHeight;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public Patients(Panel previousPanel) {
            // Dimensions
            tableWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
            tableHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
            formComponentKeyWidth = (int) (0.4 * this.rightPanelWidth);
            formComponentValueWidth = (int) (0.5 * this.rightPanelWidth);
            formComponentHorizontalMargin = (int) (0.1 * this.rightPanelWidth - 2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init controller
            this.controller = new PatientsController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "patientsMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.PERSIAN_INDIGO,
                "Regjistrimi i Pacientëve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/operator.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Patients table
            this.tableLocation = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, 80);
            this.tableSize = new Size(
                this.tableWidth,
                this.tableHeight
            );

            this.PatientsTable = new DynamicPatientsTable(
                this.tableSize,
                this.tableLocation,
                this.PatientsList,
                this.controller
            );
            this.Panel.Controls.Add(this.PatientsTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Shtimi dhe përditësimi i pacientëve";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.rightPanelWidth),
                80
            );
            right.Size = new Size(this.rightPanelWidth, this.tableHeight);
            right.FlatStyle = FlatStyle.Flat;
            right.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Panel.Controls.Add(this.right);

            // Selected patient label
            this.patientLabel = new Label();
            this.patientLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.patientLabel.Width = this.formComponentKeyWidth;
            this.patientLabel.Height = this.formComponentHeight;
            this.patientLabel.Text = "Pacienti";
            this.patientLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.patientLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.patientLabel);

            this.PatientLabelValue = new Label();
            this.PatientLabelValue.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.PatientLabelValue.Width = this.formComponentValueWidth;
            this.PatientLabelValue.Height = this.formComponentHeight;
            this.PatientLabelValue.Text = this.SelectedPatient != null ? this.SelectedPatient : "-";
            this.PatientLabelValue.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.PatientLabelValue.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.PatientLabelValue);

            /* Init form components */

            // Patient name
            this.firstNameLabel = new Label();
            this.firstNameLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.firstNameLabel.Width = this.formComponentKeyWidth;
            this.firstNameLabel.Height = this.formComponentHeight;
            this.firstNameLabel.Text = "Emri";
            this.firstNameLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.firstNameLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.firstNameLabel);

            this.FirstNameTxtBox = new TextBox();
            this.FirstNameTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FirstNameTxtBox.Width = this.formComponentValueWidth;
            this.FirstNameTxtBox.Height = this.formComponentHeight;
            this.FirstNameTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FirstNameTxtBox);

            // Patient surname
            this.lastNameLabel = new Label();
            this.lastNameLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.lastNameLabel.Width = this.formComponentKeyWidth;
            this.lastNameLabel.Height = this.formComponentHeight;
            this.lastNameLabel.Text = "Mbiemri";
            this.lastNameLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.lastNameLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.lastNameLabel);

            this.LastNameTxtBox = new TextBox();
            this.LastNameTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.LastNameTxtBox.Width = this.formComponentValueWidth;
            this.LastNameTxtBox.Height = this.formComponentHeight;
            this.LastNameTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.LastNameTxtBox);

            // Patient date of birth
            this.dateOfBirthLabel = new Label();
            this.dateOfBirthLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.dateOfBirthLabel.Width = this.formComponentKeyWidth;
            this.dateOfBirthLabel.Height = this.formComponentHeight;
            this.dateOfBirthLabel.Text = "Datëlindja";
            this.dateOfBirthLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.dateOfBirthLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.dateOfBirthLabel);

            this.DateOfBirth = new DateTimePicker();
            this.DateOfBirth.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.DateOfBirth.Width = this.formComponentValueWidth;
            this.DateOfBirth.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.DateOfBirth);

            // Patient phone number
            this.phoneNumberLabel = new Label();
            this.phoneNumberLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.phoneNumberLabel.Width = this.formComponentKeyWidth;
            this.phoneNumberLabel.Height = this.formComponentHeight;
            this.phoneNumberLabel.Text = "Telefon";
            this.phoneNumberLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.phoneNumberLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.phoneNumberLabel);

            this.PhoneNumberTxtBox = new TextBox();
            this.PhoneNumberTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.PhoneNumberTxtBox.Width = this.formComponentValueWidth;
            this.PhoneNumberTxtBox.Height = this.formComponentHeight;
            this.PhoneNumberTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.PhoneNumberTxtBox);

            // Init gender
            this.genderLabel = new Label();
            this.genderLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.genderLabel.Width = this.formComponentKeyWidth;
            this.genderLabel.Height = this.formComponentHeight;
            this.genderLabel.Text = "Gjinia";
            this.genderLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.genderLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.genderLabel);

            Point cBoxLocation = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            Size cBoxSize = new Size(this.formComponentValueWidth, this.formComponentHeight);
            this.CBox = new DynamicComboBox(
                cBoxSize,
                cBoxLocation
            );
            this.right.Controls.Add(CBox.comboBox);

            // Patient address
            this.addressLabel = new Label();
            this.addressLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.addressLabel.Width = this.formComponentKeyWidth;
            this.addressLabel.Height = this.formComponentHeight;
            this.addressLabel.Text = "Adresa";
            this.addressLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.addressLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.addressLabel);

            this.AddressTxtBox = new TextBox();
            this.AddressTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.AddressTxtBox.Width = this.formComponentValueWidth;
            this.AddressTxtBox.Height = 3 * this.formComponentHeight;
            this.AddressTxtBox.Multiline = true;
            this.AddressTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.AddressTxtBox);

            /* Buttons */

            this.resetBtn = new Button();
            this.resetBtn.Size = new Size(this.formComponentKeyWidth, this.formComponentHeight);
            this.resetBtn.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                this.tableHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.resetBtn.Text = "RESET";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.resetBtn.ForeColor = Colors.WHITE;
            this.resetBtn.BackColor = Colors.IMPERIAL_RED;
            this.resetBtn.FlatStyle = FlatStyle.Flat;
            this.resetBtn.Click += new EventHandler(onResetButtonClicked);
            this.resetBtn.Image = Image.FromFile("../../Resources/clear.png");
            this.resetBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.resetBtn);

            this.submitBtn = new Button();
            this.submitBtn.Size = new Size(this.formComponentKeyWidth, this.formComponentHeight);
            this.submitBtn.Location = new Point(
                this.rightPanelWidth - (this.formComponentKeyWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.tableHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.submitBtn.Text = "RUAJ";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.submitBtn.ForeColor = Colors.WHITE;
            this.submitBtn.BackColor = Colors.MALACHITE;
            this.submitBtn.FlatStyle = FlatStyle.Flat;
            this.submitBtn.Click += new EventHandler(onSubmitButtonClicked);
            this.submitBtn.Image = Image.FromFile("../../Resources/save.png");
            this.submitBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.submitBtn);
        }

        /**
         * Method to initialize components and fetch necessary data
         */

        public void readInitialData() {
            this.controller.init();
        }

        /**
         * Event handlers
         */

        private void onResetButtonClicked(object sender, EventArgs eventArgs) {
            controller.handleResetButton();
        }

        private void onSubmitButtonClicked(object sender, EventArgs eventArgs) {
            controller.handleSubmitButton();
        }
    }
}

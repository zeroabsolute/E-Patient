using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Manager;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Manager {
    class Users {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public DynamicComboBox CBox { get; set; }
        public DynamicTable UsersTable { get; set; }
        public Label FormRoleLabel { get; set; }
        public TextBox FormEmailTxtBox { get; set; }
        public TextBox FormPasswordTxtBox { get; set; }
        public TextBox FormFirstNameTxtBox { get; set; }
        public TextBox FormLastNameTxtBox { get; set; }
        public DateTimePicker FormDOBPicker { get; set; }
        public TextBox FormAddressTxtBox { get; set; }
        public TextBox FormPhoneNumberTxtBox { get; set; }
        public Label FormAddressLabel { get; set; }
        public Label FormSpecializationLabel { get; set; }
        public Label FormPhoneNumberLabel { get; set; }
        public DynamicComboBox SpecializationCBox { get; set; }
        public Button SubmitBtn { get; set; }
        public Button ClearBtn { get; set; }
        public Button DeleteBtn { get; set; }

        public string SelectedRole { get; set; }
        public List<Models.Operator> Operators { get; set; }
        public List<Models.Doctor> Doctors { get; set; }
        public List<Models.Nurse> Nurses { get; set; }

        private NavigationBar header;
        private UsersController controller;
        private GroupBox left;
        private GroupBox right;
        private Label selectRoleLabel;
        private Label formRoleLabel;
        private Label formEmailLabel;
        private Label formPasswordLabel;
        private Label formFirstNameLabel;
        private Label formLastNameLabel;
        private Label formDateOfBirthLabel;

        private int cardHeight = Dimensions.PANEL_HEIGHT - 100;
        private int bigCardWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
        private int smallCardWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
        private int bigCardKeyWidth;
        private int bigCardValueWidth;
        private int smallCardKeyWidth;
        private int buttonWidth;
        private int smallCardValueWidth;
        private int formComponentHeight;
        private int keyValueMargin = 50;
        private int smallKeyValueMargin = 30;
        private int formComponentVerticalMargin;
        private Point tableLocation;
        private Size tableSize;

        public Users(Panel previousPanel) {
            controller = new UsersController(this);

            bigCardKeyWidth = (int) (bigCardWidth / 2) - this.keyValueMargin;
            bigCardValueWidth = (int) (bigCardWidth / 2);
            smallCardKeyWidth = (int) (smallCardWidth / 2) - this.keyValueMargin;
            smallCardValueWidth = (int) (smallCardWidth / 2);
            formComponentHeight = 40;
            formComponentVerticalMargin = formComponentHeight + 10;
            buttonWidth = 160;

            this.tableLocation = new Point(Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 100);
            this.tableSize = new Size(
                this.bigCardWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.cardHeight - 110
            );

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "usersMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.BAHAMA_BLUE,
                "Menaxhimi i përdoruesve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/manager.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Init left container
            left = new GroupBox();
            left.Text = "Lista e përdoruesve të regjistruar";
            left.Location = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL);
            left.Size = new Size(this.bigCardWidth, this.cardHeight);
            left.FlatStyle = FlatStyle.Flat;
            left.Font = new Font(Fonts.primary, 12, FontStyle.Regular);

            this.Panel.Controls.Add(left);

            // Init role label
            this.selectRoleLabel = new Label();
            this.selectRoleLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.selectRoleLabel.Width = this.bigCardKeyWidth;
            this.selectRoleLabel.Height = this.formComponentHeight;
            this.selectRoleLabel.Text = "Roli";
            this.selectRoleLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.selectRoleLabel.ForeColor = Colors.BLACK;

            this.left.Controls.Add(this.selectRoleLabel);

            // Init combo box
            Point cBoxLocation = new Point(
                this.bigCardKeyWidth + (this.keyValueMargin - Dimensions.PANEL_CARD_PADDING_HORIZONTAL), 
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            Size cBoxSize = new Size(this.bigCardValueWidth, this.formComponentHeight);
            this.CBox = new DynamicComboBox(
                cBoxSize,
                cBoxLocation
            );
            this.CBox.comboBox.SelectedIndexChanged += new EventHandler(this.onRoleChanged);

            this.left.Controls.Add(CBox.comboBox);

            // Init datagrid view for showing users
            this.UsersTable = new DynamicTable(
                this.tableSize, 
                this.tableLocation,
                this.Operators,
                this.Doctors,
                this.Nurses,
                this.controller
            );

            this.left.Controls.Add(this.UsersTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Shtimi i përdoruesve";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.smallCardWidth), 
                Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL
            );
            right.Size = new Size(this.smallCardWidth, this.cardHeight);
            right.FlatStyle = FlatStyle.Flat;
            right.Font = new Font(Fonts.primary, 12, FontStyle.Regular);

            this.Panel.Controls.Add(right);

            /* Init form components */

            // Role
            this.formRoleLabel = new Label();
            this.formRoleLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.formRoleLabel.Width = this.smallCardKeyWidth;
            this.formRoleLabel.Height = this.formComponentHeight;
            this.formRoleLabel.Text = "Roli";
            this.formRoleLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.formRoleLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.formRoleLabel);

            this.FormRoleLabel = new Label();
            this.FormRoleLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.FormRoleLabel.Width = this.smallCardKeyWidth;
            this.FormRoleLabel.Height = this.formComponentHeight;
            this.FormRoleLabel.Text = this.SelectedRole != null ? this.SelectedRole : "-";
            this.FormRoleLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.FormRoleLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.FormRoleLabel);

            // Email
            this.formEmailLabel = new Label();
            this.formEmailLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 
                this.formComponentVerticalMargin + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.formEmailLabel.Width = this.smallCardKeyWidth;
            this.formEmailLabel.Height = this.formComponentHeight;
            this.formEmailLabel.Text = "Email";
            this.formEmailLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.formEmailLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.formEmailLabel);

            this.FormEmailTxtBox = new TextBox();
            this.FormEmailTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                this.formComponentVerticalMargin + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormEmailTxtBox.Width = this.smallCardValueWidth;
            this.FormEmailTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FormEmailTxtBox);

            // Password
            this.formPasswordLabel = new Label();
            this.formPasswordLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.formPasswordLabel.Width = this.smallCardKeyWidth;
            this.formPasswordLabel.Height = this.formComponentHeight;
            this.formPasswordLabel.Text = "Fjalëkalimi";
            this.formPasswordLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.formPasswordLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.formPasswordLabel);

            this.FormPasswordTxtBox = new TextBox();
            this.FormPasswordTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormPasswordTxtBox.Width = this.smallCardValueWidth;
            this.FormPasswordTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.FormPasswordTxtBox.PasswordChar = '*';
            this.right.Controls.Add(this.FormPasswordTxtBox);

            // First name
            this.formFirstNameLabel = new Label();
            this.formFirstNameLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.formFirstNameLabel.Width = this.smallCardKeyWidth;
            this.formFirstNameLabel.Height = this.formComponentHeight;
            this.formFirstNameLabel.Text = "Emri";
            this.formFirstNameLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.formFirstNameLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.formFirstNameLabel);

            this.FormFirstNameTxtBox = new TextBox();
            this.FormFirstNameTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormFirstNameTxtBox.Width = this.smallCardValueWidth;
            this.FormFirstNameTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FormFirstNameTxtBox);

            // Last name
            this.formLastNameLabel = new Label();
            this.formLastNameLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.formLastNameLabel.Width = this.smallCardKeyWidth;
            this.formLastNameLabel.Height = this.formComponentHeight;
            this.formLastNameLabel.Text = "Mbiemri";
            this.formLastNameLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.formLastNameLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.formLastNameLabel);

            this.FormLastNameTxtBox = new TextBox();
            this.FormLastNameTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormLastNameTxtBox.Width = this.smallCardValueWidth;
            this.FormLastNameTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FormLastNameTxtBox);

            // Date of birth
            this.formDateOfBirthLabel = new Label();
            this.formDateOfBirthLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.formDateOfBirthLabel.Width = this.smallCardKeyWidth;
            this.formDateOfBirthLabel.Height = this.formComponentHeight;
            this.formDateOfBirthLabel.Text = "Datëlindja";
            this.formDateOfBirthLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.formDateOfBirthLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.formDateOfBirthLabel);

            this.FormDOBPicker = new DateTimePicker();
            this.FormDOBPicker.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormDOBPicker.Width = this.smallCardValueWidth;
            this.FormDOBPicker.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FormDOBPicker);

            // Phone number
            this.FormPhoneNumberLabel = new Label();
            this.FormPhoneNumberLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormPhoneNumberLabel.Width = this.smallCardKeyWidth;
            this.FormPhoneNumberLabel.Height = this.formComponentHeight;
            this.FormPhoneNumberLabel.Text = "Telefon";
            this.FormPhoneNumberLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.FormPhoneNumberLabel.ForeColor = Colors.BLACK;
            this.FormPhoneNumberLabel.Visible = false;
            this.right.Controls.Add(this.FormPhoneNumberLabel);

            this.FormPhoneNumberTxtBox = new TextBox();
            this.FormPhoneNumberTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormPhoneNumberTxtBox.Width = this.smallCardValueWidth;
            this.FormPhoneNumberTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.FormPhoneNumberTxtBox.Visible = false;
            this.right.Controls.Add(this.FormPhoneNumberTxtBox);

            // Address
            this.FormAddressLabel = new Label();
            this.FormAddressLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormAddressLabel.Width = this.smallCardKeyWidth;
            this.FormAddressLabel.Height = this.formComponentHeight;
            this.FormAddressLabel.Text = "Adresa";
            this.FormAddressLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.FormAddressLabel.ForeColor = Colors.BLACK;
            this.FormAddressLabel.Visible = false;
            this.right.Controls.Add(this.FormAddressLabel);

            this.FormAddressTxtBox = new TextBox();
            this.FormAddressTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormAddressTxtBox.Width = this.smallCardValueWidth;
            this.FormAddressTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.FormAddressTxtBox.Visible = false;
            this.right.Controls.Add(this.FormAddressTxtBox);

            // Specialization
            this.FormSpecializationLabel = new Label();
            this.FormSpecializationLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FormSpecializationLabel.Width = this.smallCardKeyWidth;
            this.FormSpecializationLabel.Height = this.formComponentHeight;
            this.FormSpecializationLabel.Text = "Specializimi";
            this.FormSpecializationLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.FormSpecializationLabel.ForeColor = Colors.BLACK;
            this.FormSpecializationLabel.Visible = false;
            this.right.Controls.Add(this.FormSpecializationLabel);

            Point cBoxLocation2 = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.smallCardKeyWidth + this.smallKeyValueMargin,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            Size cBoxSize2 = new Size(this.smallCardValueWidth, this.formComponentHeight);
            this.SpecializationCBox = new DynamicComboBox(
                cBoxSize2,
                cBoxLocation2
            );
            this.right.Controls.Add(SpecializationCBox.comboBox);

            /* Buttons */

            this.DeleteBtn = new Button();
            this.DeleteBtn.Name = "deleteButton";
            this.DeleteBtn.Size = new Size(this.buttonWidth, this.formComponentHeight);
            this.DeleteBtn.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.DeleteBtn.Text = "ÇAKTIVIZO";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.DeleteBtn.ForeColor = Colors.WHITE;
            this.DeleteBtn.BackColor = Colors.IMPERIAL_RED;
            this.DeleteBtn.FlatStyle = FlatStyle.Flat;
            this.DeleteBtn.Click += new EventHandler(onDeactivateButtonClicked);
            this.DeleteBtn.Image = Image.FromFile("../../Resources/delete.png");
            this.DeleteBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.DeleteBtn);

            this.ClearBtn = new Button();
            this.ClearBtn.Name = "clearButton";
            this.ClearBtn.Size = new Size(this.buttonWidth, this.formComponentHeight);
            this.ClearBtn.Location = new Point(
                190,
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.ClearBtn.Text = "RESET";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.ClearBtn.ForeColor = Colors.WHITE;
            this.ClearBtn.BackColor = Colors.SALMON_RED;
            this.ClearBtn.FlatStyle = FlatStyle.Flat;
            this.ClearBtn.Click += new EventHandler(onClearButtonClicked);
            this.ClearBtn.Image = Image.FromFile("../../Resources/clear.png");
            this.ClearBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.ClearBtn);

            this.SubmitBtn = new Button();
            this.SubmitBtn.Name = "submitButton";
            this.SubmitBtn.Size = new Size(this.buttonWidth, this.formComponentHeight);
            this.SubmitBtn.Location = new Point(
                this.smallCardWidth - (this.buttonWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.SubmitBtn.Text = "RUAJ";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.SubmitBtn.ForeColor = Colors.WHITE;
            this.SubmitBtn.BackColor = Colors.MALACHITE;
            this.SubmitBtn.FlatStyle = FlatStyle.Flat;
            this.SubmitBtn.Click += new EventHandler(onSubmitButtonClicked);
            this.SubmitBtn.Image = Image.FromFile("../../Resources/save.png");
            this.SubmitBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.SubmitBtn);
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

        private void onRoleChanged(object sender, EventArgs eventArgs) {
            this.controller.handleRoleSelection();
        }

        private void onDeactivateButtonClicked(object sender, EventArgs eventArgs) {
            this.controller.handleDeactivate();
        }

        private void onClearButtonClicked(object sender, EventArgs eventArgs) {
            this.controller.handleClear();
        }

        private void onSubmitButtonClicked(object sender, EventArgs eventArgs) {
            this.controller.handleSubmit();
        }
    }
}

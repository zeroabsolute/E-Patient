using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Operator;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Operator {
    class Reservations {
        public User LoggedInUser { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public TextBox SearchTermTxtBox { get; set; }
        public ReservationsTable ReservationsTable { get; set; }
        public List<Models.Reservation> ReservationsList { get; set; }
        public Models.Reservation SelectedReservation { get; set; }
        public Label SelectedReservationLabel { get; set; }
        public DateTimePicker StartDateTime { get; set; }
        public DateTimePicker EndDateTime { get; set; }
        public DynamicComboBox ServiceCBox { get; set; }
        public DynamicComboBox PatientCBox { get; set; }
        public DynamicComboBox DoctorCBox { get; set; }
        public DynamicComboBox NurseCBox { get; set; }

        private ReservationsController controller;
        private NavigationBar header;
        private GroupBox right;
        private GroupBox left;
        private Label searchLabel;
        private Button printReservationBtn;
        private Button editBtn;
        private Label selectedReservationLabel;
        private Label startDateTimeLabel;
        private Label endDateTimeLabel;
        private Label serviceLabel;
        private Label patientLabel;
        private Label doctorLabel;
        private Label nurseLabel;
        private Button resetBtn;
        private Button submitBtn;

        private int cardHeight;
        private int leftPanelWidth;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;
        private Point tableLocation;
        private Size tableSize;

        public Reservations (Panel previousPanel) {
            controller = new ReservationsController(this);

            // Dimensions
            leftPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
            cardHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
            formComponentKeyWidth = (int) (0.3 * this.rightPanelWidth);
            formComponentValueWidth = (int) (0.6 * this.rightPanelWidth);
            formComponentHorizontalMargin = (int) (0.1 * this.rightPanelWidth - 2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);
            Size cBoxSize = new Size(this.formComponentValueWidth, this.formComponentHeight);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "reservationsMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.PERSIAN_INDIGO,
                "Menaxhimi i rezervimeve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/operator.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Init left container
            left = new GroupBox();
            left.Text = "Lista e rezervimeve të kryera";
            left.Location = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL);
            left.Size = new Size(this.leftPanelWidth, this.cardHeight);
            left.FlatStyle = FlatStyle.Flat;
            left.Font = new Font(Fonts.primary, 12, FontStyle.Regular);

            this.Panel.Controls.Add(left);

            // Init search label
            this.searchLabel = new Label();
            this.searchLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.searchLabel.Width = this.formComponentKeyWidth;
            this.searchLabel.Height = this.formComponentHeight;
            this.searchLabel.Text = "Kërkim";
            this.searchLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.searchLabel.ForeColor = Colors.BLACK;

            this.left.Controls.Add(this.searchLabel);

            // Init search term text box
            this.SearchTermTxtBox = new TextBox();
            this.SearchTermTxtBox.Location = new Point(
                this.formComponentKeyWidth + (this.formComponentHorizontalMargin - Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.SearchTermTxtBox.Width = this.leftPanelWidth - (this.formComponentHorizontalMargin + this.formComponentKeyWidth);
            this.SearchTermTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.SearchTermTxtBox.TextChanged += new EventHandler(this.onSearchTermChanged);
            this.left.Controls.Add(this.SearchTermTxtBox);

            // Reservations table
            Point tableLocation = new Point(Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 100);
            Size tableSize = new Size(
                this.leftPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.cardHeight - 180
            );

            this.tableLocation = tableLocation;
            this.tableSize = tableSize;

            this.ReservationsTable = new ReservationsTable(
                this.tableSize,
                this.tableLocation,
                this.ReservationsList,
                this.controller
            );
            this.left.Controls.Add(this.ReservationsTable.DataGrid);

            // Buttons for printing

            this.printReservationBtn = new Button();
            this.printReservationBtn.Size = new Size(2 * this.formComponentKeyWidth, this.formComponentHeight);
            this.printReservationBtn.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.printReservationBtn.Text = "PRINTO REZERVIMIN";
            this.printReservationBtn.UseVisualStyleBackColor = true;
            this.printReservationBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.printReservationBtn.ForeColor = Colors.WHITE;
            this.printReservationBtn.BackColor = Colors.PERSIAN_INDIGO;
            this.printReservationBtn.FlatStyle = FlatStyle.Flat;
            this.printReservationBtn.Click += new EventHandler(onReservationPrintClicked);
            this.printReservationBtn.Image = Image.FromFile("../../Resources/print.png");
            this.printReservationBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.left.Controls.Add(this.printReservationBtn);

            this.editBtn = new Button();
            this.editBtn.Size = new Size(2 * this.formComponentKeyWidth, this.formComponentHeight);
            this.editBtn.Location = new Point(
                this.leftPanelWidth - (2 * this.formComponentKeyWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.editBtn.Text = "PËRDITËSO";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.editBtn.ForeColor = Colors.WHITE;
            this.editBtn.BackColor = Colors.PERSIAN_INDIGO;
            this.editBtn.FlatStyle = FlatStyle.Flat;
            this.editBtn.Click += new EventHandler(onEditClicked);
            this.editBtn.Image = Image.FromFile("../../Resources/edit.png");
            this.editBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.left.Controls.Add(this.editBtn);

            // Init right container
            right = new GroupBox();
            right.Text = "Shtimi dhe përditësimi i rezervimeve";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.rightPanelWidth),
                80
            );
            right.Size = new Size(this.rightPanelWidth, this.cardHeight);
            right.FlatStyle = FlatStyle.Flat;
            right.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Panel.Controls.Add(this.right);

            // Selected reservation label
            this.selectedReservationLabel = new Label();
            this.selectedReservationLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.selectedReservationLabel.Width = this.formComponentKeyWidth;
            this.selectedReservationLabel.Height = this.formComponentHeight;
            this.selectedReservationLabel.Text = "Rezervimi";
            this.selectedReservationLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.selectedReservationLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.selectedReservationLabel);

            this.SelectedReservationLabel = new Label();
            this.SelectedReservationLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.SelectedReservationLabel.Width = this.formComponentValueWidth;
            this.SelectedReservationLabel.Height = this.formComponentHeight;
            this.SelectedReservationLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.SelectedReservationLabel.ForeColor = Colors.BLACK;
            this.SelectedReservationLabel.Text =  "-";
            this.right.Controls.Add(this.SelectedReservationLabel);

            // Reservation start datetime
            this.startDateTimeLabel = new Label();
            this.startDateTimeLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.startDateTimeLabel.Width = this.formComponentKeyWidth;
            this.startDateTimeLabel.Height = this.formComponentHeight;
            this.startDateTimeLabel.Text = "Fillimi";
            this.startDateTimeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.startDateTimeLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.startDateTimeLabel);

            this.StartDateTime = new DateTimePicker();
            this.StartDateTime.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.StartDateTime.Width = this.formComponentValueWidth;
            this.StartDateTime.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.StartDateTime.Format = DateTimePickerFormat.Custom;
            this.StartDateTime.CustomFormat = "dddd, dd-MM-yyyy,  HH:mm";
            this.right.Controls.Add(this.StartDateTime);

            // Reservation end datetime
            this.endDateTimeLabel = new Label();
            this.endDateTimeLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.endDateTimeLabel.Width = this.formComponentKeyWidth;
            this.endDateTimeLabel.Height = this.formComponentHeight;
            this.endDateTimeLabel.Text = "Përfundimi";
            this.endDateTimeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.endDateTimeLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.endDateTimeLabel);

            this.EndDateTime = new DateTimePicker();
            this.EndDateTime.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.EndDateTime.Width = this.formComponentValueWidth;
            this.EndDateTime.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.EndDateTime.Format = DateTimePickerFormat.Custom;
            this.EndDateTime.CustomFormat = "dddd, dd-MM-yyyy,  HH:mm";
            this.right.Controls.Add(this.EndDateTime);

            // Init service
            this.serviceLabel = new Label();
            this.serviceLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.serviceLabel.Width = this.formComponentKeyWidth;
            this.serviceLabel.Height = this.formComponentHeight;
            this.serviceLabel.Text = "Shërbimi";
            this.serviceLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.serviceLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.serviceLabel);

            this.ServiceCBox = new DynamicComboBox(
                cBoxSize,
                new Point(
                    Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                    (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
                )
            );
            this.right.Controls.Add(ServiceCBox.comboBox);

            // Init patient
            this.patientLabel = new Label();
            this.patientLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.patientLabel.Width = this.formComponentKeyWidth;
            this.patientLabel.Height = this.formComponentHeight;
            this.patientLabel.Text = "Pacienti";
            this.patientLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.patientLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.patientLabel);
            
            this.PatientCBox = new DynamicComboBox(
                cBoxSize,
                new Point(
                    Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                    (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
                )
            );
            this.right.Controls.Add(PatientCBox.comboBox);

            // Init doctor
            this.doctorLabel = new Label();
            this.doctorLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.doctorLabel.Width = this.formComponentKeyWidth;
            this.doctorLabel.Height = this.formComponentHeight;
            this.doctorLabel.Text = "Mjeku";
            this.doctorLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.doctorLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.doctorLabel);

            this.DoctorCBox = new DynamicComboBox(
                cBoxSize,
                new Point(
                    Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                    (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
                )
            );
            this.right.Controls.Add(DoctorCBox.comboBox);

            // Init nurse
            this.nurseLabel = new Label();
            this.nurseLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.nurseLabel.Width = this.formComponentKeyWidth;
            this.nurseLabel.Height = this.formComponentHeight;
            this.nurseLabel.Text = "Infermieri";
            this.nurseLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.nurseLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.nurseLabel);

            this.NurseCBox = new DynamicComboBox(
                cBoxSize,
                new Point(
                    Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                    (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
                )
            );
            this.right.Controls.Add(NurseCBox.comboBox);

            // Buttons

            this.resetBtn = new Button();
            this.resetBtn.Size = new Size(this.formComponentKeyWidth, this.formComponentHeight);
            this.resetBtn.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
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
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
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

        private void onSearchTermChanged(object sender, EventArgs e) {
            this.controller.handleSearch();
        }

        private void onReservationPrintClicked(object sender, EventArgs e) {
            this.controller.handlePrintReservation();
        }

        private void onEditClicked(object sender, EventArgs e) {
            this.controller.handleEdit();
        }

        private void onResetButtonClicked(object sender, EventArgs e) {
            this.controller.handleResetButton();
        }

        private void onSubmitButtonClicked(object sender, EventArgs e) {
            this.controller.handleSubmitButton();
        }
    }
}

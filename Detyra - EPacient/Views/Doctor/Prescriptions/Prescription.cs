using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Doctor;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Doctor {
    class Prescription {
        public User LoggedInUser { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public TextBox SearchTermTxtBox { get; set; }
        public ReservationsTable ReservationsTable { get; set; }
        public Label SelectedReservationLabel { get; set; }
        public TextBox DescriptionTxtBox { get; set; }
        public ListBox MedicamentsListBox { get; set; }
        public Button SubmitBtn { get; set; }

        private PrescriptionsController controller;
        private NavigationBar header;
        private GroupBox right;
        private GroupBox left;
        private Label searchLabel;
        private Label selectedReservationLabel;
        private Button printPrescriptionBtn;
        private Label descriptionLabel;
        private Label medicamentsLabel;
        private Button resetBtn;

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

        public Prescription(Panel previousPanel) {
            controller = new PrescriptionsController(this);

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
            this.Panel.Name = "prescriptionMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.FOREST,
                "Recetat",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/surgeon.png"
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
                this.controller
            );
            this.left.Controls.Add(this.ReservationsTable.DataGrid);

            // Buttons for printing and creating prescription

            this.printPrescriptionBtn = new Button();
            this.printPrescriptionBtn.Size = new Size(2 * this.formComponentKeyWidth, this.formComponentHeight);
            this.printPrescriptionBtn.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                this.cardHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.printPrescriptionBtn.Text = "PRINTO RECETËN";
            this.printPrescriptionBtn.UseVisualStyleBackColor = true;
            this.printPrescriptionBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.printPrescriptionBtn.ForeColor = Colors.WHITE;
            this.printPrescriptionBtn.BackColor = Colors.PEPER_GREEN;
            this.printPrescriptionBtn.FlatStyle = FlatStyle.Flat;
            this.printPrescriptionBtn.Click += new EventHandler(onPrescriptionPrintClicked);
            this.printPrescriptionBtn.Image = Image.FromFile("../../Resources/print.png");
            this.printPrescriptionBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.left.Controls.Add(this.printPrescriptionBtn);

            // Init right container
            right = new GroupBox();
            right.Text = "Shtimi dhe përditësimi i recetave";
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
            this.SelectedReservationLabel.Text = "-";
            this.right.Controls.Add(this.SelectedReservationLabel);

            // Receipt description
            this.descriptionLabel = new Label();
            this.descriptionLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (1 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.descriptionLabel.Width = this.formComponentKeyWidth;
            this.descriptionLabel.Height = this.formComponentHeight;
            this.descriptionLabel.Text = "Përshkrimi";
            this.descriptionLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.descriptionLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.descriptionLabel);

            this.DescriptionTxtBox = new TextBox();
            this.DescriptionTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (1 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.DescriptionTxtBox.Width = this.formComponentValueWidth;
            this.DescriptionTxtBox.Height = (int) (3 * this.formComponentHeight);
            this.DescriptionTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.DescriptionTxtBox.Multiline = true;
            this.right.Controls.Add(this.DescriptionTxtBox);

            // Medicaments
            this.medicamentsLabel = new Label();
            this.medicamentsLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.medicamentsLabel.Width = this.formComponentKeyWidth;
            this.medicamentsLabel.Height = this.formComponentHeight;
            this.medicamentsLabel.Text = "Medikamentet";
            this.medicamentsLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.medicamentsLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.medicamentsLabel);

            this.MedicamentsListBox = new ListBox();
            this.MedicamentsListBox.Size = new Size(
                this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL), 
                240
            );
            this.MedicamentsListBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MedicamentsListBox.MultiColumn = false;
            this.MedicamentsListBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.MedicamentsListBox.ForeColor = Colors.BLACK;
            this.MedicamentsListBox.SelectionMode = SelectionMode.MultiExtended;
            this.MedicamentsListBox.DisplayMember = "name";
            this.MedicamentsListBox.ValueMember = "id";
            this.right.Controls.Add(this.MedicamentsListBox);

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

            this.SubmitBtn = new Button();
            this.SubmitBtn.Size = new Size(this.formComponentKeyWidth, this.formComponentHeight);
            this.SubmitBtn.Location = new Point(
                this.rightPanelWidth - (this.formComponentKeyWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
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

        private void onSearchTermChanged(object sender, EventArgs e) {
            this.controller.handleSearch();
        }

        private void onPrescriptionPrintClicked(object sender, EventArgs e) {
            this.controller.handlePrintPrescription();
        }

        private void onSubmitButtonClicked(object sender, EventArgs e) {
            this.controller.handleSubmitButton();
        }

        private void onResetButtonClicked(object sender, EventArgs e) {
            this.controller.handleResetButton();
        }
    }
}

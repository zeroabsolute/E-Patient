using System;
using System.Collections.Generic;
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
    class DoctorInCharge {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public EmergencyDoctorsTable EmergencyDoctorsTable { get; set; }
        public DynamicComboBox SectorsCBox { get; set; }
        public DynamicComboBox MonthCBox { get; set; }
        public TextBox YearTxtBox { get; set; }
        public Button SubmitBtn { get; set; }

        private NavigationBar header;
        private EmergencyDoctorsController controller;
        private Label sectorLabel;
        private Label monthLabel;
        private Label yearLabel;

        public DoctorInCharge(Panel previousPanel) {
            // Dimensions
            int tableWidth = Dimensions.PANEL_WIDTH - (2 * Dimensions.PANEL_PADDING_HORIZONTAL);
            int tableHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 130);
            int formComponentWidth = (int) Dimensions.PANEL_WIDTH / 6;
            int formComponentLabelWidth = 80;
            int formComponentHeight = 30;
            Size cBoxSize = new Size(formComponentWidth, formComponentHeight);

            // Controller
            this.controller = new EmergencyDoctorsController(this);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "doctorInChargeMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.BAHAMA_BLUE,
                "Menaxhimi i mjekëve roje",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/manager.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Sector selection
            this.sectorLabel = new Label();
            this.sectorLabel.Location = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL,
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.sectorLabel.Size = new Size(
                formComponentLabelWidth,
                formComponentHeight
            );
            this.sectorLabel.Text = "Sektori";
            this.sectorLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.sectorLabel.ForeColor = Colors.BLACK;
            this.sectorLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel.Controls.Add(this.sectorLabel);

            Point sectorCBoxLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL + formComponentLabelWidth,
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.SectorsCBox = new DynamicComboBox(
                cBoxSize,
                sectorCBoxLocation
            );
            this.SectorsCBox.comboBox.SelectedIndexChanged += new EventHandler(this.onFilterChanged);
            this.Panel.Controls.Add(SectorsCBox.comboBox);

            // Month selection
            this.monthLabel = new Label();
            this.monthLabel.Location = new Point(
                (int) (1.7 * formComponentWidth),
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.monthLabel.Size = new Size(
                formComponentLabelWidth,
                formComponentHeight
            );
            this.monthLabel.Text = "Muaji";
            this.monthLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.monthLabel.ForeColor = Colors.BLACK;
            this.monthLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel.Controls.Add(this.monthLabel);

            Point monthCBoxLocation = new Point(
                (int) (2.1 * formComponentWidth),
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.MonthCBox = new DynamicComboBox(
                cBoxSize,
                monthCBoxLocation
            );
            this.MonthCBox.comboBox.SelectedIndexChanged += new EventHandler(this.onFilterChanged);
            this.Panel.Controls.Add(MonthCBox.comboBox);

            // Year selection
            this.yearLabel = new Label();
            this.yearLabel.Location = new Point(
                (int) (3.4 * formComponentWidth),
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.yearLabel.Size = new Size(
                formComponentLabelWidth,
                formComponentHeight
            );
            this.yearLabel.Text = "Viti";
            this.yearLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.yearLabel.ForeColor = Colors.BLACK;
            this.yearLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.Panel.Controls.Add(this.yearLabel);

            this.YearTxtBox = new TextBox();
            this.YearTxtBox.Text = DateTime.Now.ToString(DateTimeFormats.YEAR);
            this.YearTxtBox.Location = new Point(
                (int) (3.8 * formComponentWidth),
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.YearTxtBox.Size = new Size(
                formComponentWidth,
                formComponentHeight
            );
            this.YearTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.YearTxtBox.TextChanged += new EventHandler(this.onFilterChanged);
            this.Panel.Controls.Add(this.YearTxtBox);

            // Submit button
            int buttonWidth = (int) (formComponentWidth * 0.7);

            this.SubmitBtn = new Button();
            this.SubmitBtn.Name = "submitButton";
            this.SubmitBtn.Size = new Size(
                buttonWidth,
                formComponentHeight
            );
            this.SubmitBtn.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + buttonWidth),
                2 * Dimensions.PANEL_CARD_PADDING_VERTICAL + Dimensions.NAV_BAR_HEIGHT
            );
            this.SubmitBtn.Text = "GJENERO";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.SubmitBtn.ForeColor = Colors.WHITE;
            this.SubmitBtn.BackColor = Colors.MALACHITE;
            this.SubmitBtn.FlatStyle = FlatStyle.Flat;
            this.SubmitBtn.Click += new EventHandler(onSubmitButtonClicked);
            this.SubmitBtn.Image = Image.FromFile("../../Resources/done.png");
            this.SubmitBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.Panel.Controls.Add(this.SubmitBtn);

            // Emergency doctors table
            Point tableLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL, 
                Dimensions.PANEL_HEIGHT - (tableHeight + Dimensions.PANEL_PADDING_HORIZONTAL)
            );
            Size tableSize = new Size(
                tableWidth,
                tableHeight
            );

            this.EmergencyDoctorsTable = new EmergencyDoctorsTable(
                tableSize,
                tableLocation,
                this.controller
            );
            this.Panel.Controls.Add(this.EmergencyDoctorsTable.DataGrid);
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

        private void onFilterChanged(object sender, EventArgs eventArgs) {
            this.controller.handleFilterChange();
        }

        private void onSubmitButtonClicked(object sender, EventArgs eventArgs) {
            this.controller.handleSubmitButton();
        }
    }
}

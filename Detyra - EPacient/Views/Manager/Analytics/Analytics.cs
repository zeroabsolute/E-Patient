using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Manager;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;
using Detyra___EPacient.Views.Manager.Analytics;

namespace Detyra___EPacient.Views.Manager {
    class ManagerAnalytics {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public Card ServicesCard { get; set; }
        public Card PatientsCard { get; set; }
        public Card DoctorsCard { get; set; }
        public Card NursesCard { get; set; }
        public DynamicComboBox DoctorsCBox { get; set; }
        public DynamicComboBox MonthCBox { get; set; }
        public TextBox YearTxtBox { get; set; }
        public DoctorReservationsTable DoctorReservationsTable { get; set; }
        public Chart DoctorReservationsChart { get; set; }
        public ChartArea ChartArea { get; set; }
        public Series ReservationsSeries { get; set; }

        private AnalyticsController controller;
        private NavigationBar header;
        private Panel bottomContainer;
        private Label bottomTitle;
        private Label doctorLabel;
        private Label monthLabel;
        private Label yearLabel;

        public ManagerAnalytics(Panel previousPanel) {
            this.controller = new AnalyticsController(this);

            // Dimensions
            int occupiedSpace = (4 * Dimensions.TOP_CARD_SIZE.Width) + 2 * Dimensions.PANEL_PADDING_HORIZONTAL;
            int cardMargin = (int) (Dimensions.PANEL_WIDTH - occupiedSpace) / 3;
            int formComponentWidth = (int) Dimensions.PANEL_WIDTH / 6;
            int formComponentLabelWidth = 100;
            int formComponentHeight = 30;
            Size cBoxSize = new Size(formComponentWidth, formComponentHeight);
            int bottomContainerWidth = Dimensions.PANEL_WIDTH - (2 * Dimensions.PANEL_PADDING_HORIZONTAL);
            int chartWidth = (int) (0.6 * bottomContainerWidth);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "analyticsMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.ALTO;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.BAHAMA_BLUE,
                "Statistika",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/manager.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            /* Top cards */

            // Services count
            Point servicesCountLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL,
                Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL
            );
            this.ServicesCard = new Card(
                Image.FromFile("../../Resources/assignment.png"),
                "Shërbime",
                Colors.LILAC_BUSH,
                servicesCountLocation
            );
            this.Panel.Controls.Add(this.ServicesCard.Container);

            // Patients count
            Point patientsCountLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL + Dimensions.TOP_CARD_SIZE.Width + cardMargin,
                Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL
            );
            this.PatientsCard = new Card(
                Image.FromFile("../../Resources/users.png"),
                "Pacientë",
                Colors.DODGER_BLUE,
                patientsCountLocation
            );
            this.Panel.Controls.Add(this.PatientsCard.Container);

            // Doctors count
            Point doctorsCountLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL + (2 * Dimensions.TOP_CARD_SIZE.Width) + (2 * cardMargin),
                Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL
            );
            this.DoctorsCard = new Card(
                Image.FromFile("../../Resources/hospital.png"),
                "Mjekë",
                Colors.POMEGRANATE,
                doctorsCountLocation
            );
            this.Panel.Controls.Add(this.DoctorsCard.Container);

            // Nurses count
            Point nursesCountLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL + (3 * Dimensions.TOP_CARD_SIZE.Width) + (3 * cardMargin),
                Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL
            );
            this.NursesCard = new Card(
                Image.FromFile("../../Resources/user.png"),
                "Infermierë",
                Colors.MINT,
                nursesCountLocation
            );
            this.Panel.Controls.Add(this.NursesCard.Container);

            /* Bottom analytics */

            // Container
            int bottomContainerHeight = Dimensions.PANEL_HEIGHT - 250;

            this.bottomContainer = new Panel();
            this.bottomContainer.AutoSize = true;
            this.bottomContainer.Location = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL,
                Dimensions.PANEL_HEIGHT - (Dimensions.PANEL_PADDING_HORIZONTAL + bottomContainerHeight)
            );
            this.bottomContainer.Name = "bottomPanel";
            this.bottomContainer.Size = new Size(
                bottomContainerWidth,
                bottomContainerHeight
            );
            this.bottomContainer.TabIndex = 0;
            this.bottomContainer.BackColor = Colors.WHITE;
            this.Panel.Controls.Add(this.bottomContainer);

            // Title
            this.bottomTitle = new Label();
            this.bottomTitle.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL
            );
            this.bottomTitle.Size = new Size(
                (int) (Dimensions.PANEL_WIDTH / 2),
                formComponentHeight
            );
            this.bottomTitle.Text = "Ngarkesa për çdo mjek";
            this.bottomTitle.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.bottomTitle.ForeColor = Colors.BLACK;
            this.bottomTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.bottomContainer.Controls.Add(this.bottomTitle);

            // Doctor selection
            this.doctorLabel = new Label();
            this.doctorLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 3
            );
            this.doctorLabel.Size = new Size(
                formComponentLabelWidth,
                formComponentHeight
            );
            this.doctorLabel.Text = "Mjeku";
            this.doctorLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.doctorLabel.ForeColor = Colors.BLACK;
            this.doctorLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.bottomContainer.Controls.Add(this.doctorLabel);

            Point doctorCBoxLocation = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + formComponentLabelWidth,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 3
            );
            this.DoctorsCBox = new DynamicComboBox(
                cBoxSize,
                doctorCBoxLocation
            );
            this.DoctorsCBox.comboBox.SelectedIndexChanged += new EventHandler(this.onFilterChanged);
            this.bottomContainer.Controls.Add(DoctorsCBox.comboBox);

            // Month selection
            this.monthLabel = new Label();
            this.monthLabel.Location = new Point(
                (int) (2.2 * formComponentWidth),
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 3
            );
            this.monthLabel.Size = new Size(
                formComponentLabelWidth,
                formComponentHeight
            );
            this.monthLabel.Text = "Muaji";
            this.monthLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.monthLabel.ForeColor = Colors.BLACK;
            this.monthLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.bottomContainer.Controls.Add(this.monthLabel);

            Point monthCBoxLocation = new Point(
                (int) (2.8 * formComponentWidth),
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 3
            );
            this.MonthCBox = new DynamicComboBox(
                cBoxSize,
                monthCBoxLocation
            );
            this.MonthCBox.comboBox.SelectedIndexChanged += new EventHandler(this.onFilterChanged);
            this.bottomContainer.Controls.Add(MonthCBox.comboBox);

            // Year selection
            this.yearLabel = new Label();
            this.yearLabel.Location = new Point(
                bottomContainerWidth - (Dimensions.PANEL_CARD_PADDING_HORIZONTAL + formComponentWidth + formComponentLabelWidth),
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 3
            );
            this.yearLabel.Size = new Size(
                formComponentLabelWidth,
                formComponentHeight
            );
            this.yearLabel.Text = "Viti";
            this.yearLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.yearLabel.ForeColor = Colors.BLACK;
            this.yearLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.bottomContainer.Controls.Add(this.yearLabel);

            this.YearTxtBox = new TextBox();
            this.YearTxtBox.Text = DateTime.Now.ToString(DateTimeFormats.YEAR);
            this.YearTxtBox.Location = new Point(
                bottomContainerWidth - (Dimensions.PANEL_CARD_PADDING_HORIZONTAL + formComponentWidth),
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 3
            );
            this.YearTxtBox.Size = new Size(
                formComponentWidth,
                formComponentHeight
            );
            this.YearTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.YearTxtBox.TextChanged += new EventHandler(this.onFilterChanged);
            this.bottomContainer.Controls.Add(this.YearTxtBox);

            // Table
            Size tableSize = new Size(
                (int) Dimensions.PANEL_WIDTH / 3,
                bottomContainerHeight - 130
            );
            Point tableLocation = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                bottomContainerHeight - (Dimensions.PANEL_CARD_PADDING_HORIZONTAL + tableSize.Height)
            );

            this.DoctorReservationsTable = new DoctorReservationsTable(
                tableSize,
                tableLocation,
                this.controller
            );
            this.bottomContainer.Controls.Add(this.DoctorReservationsTable.DataGrid);

            // Chart
            this.ChartArea = new ChartArea();
            this.ChartArea.AxisX.Enabled = AxisEnabled.True;
            this.ChartArea.AxisX.Name = "Data";
            this.ChartArea.AxisX.Minimum = 1;
            this.ChartArea.AxisX.LineColor = Colors.BLACK;
            this.ChartArea.AxisX.Maximum = 31;
            this.ChartArea.AxisX.MajorGrid.LineColor = Colors.DOVE_GRAY;
            this.ChartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            this.ChartArea.AxisY.Enabled = AxisEnabled.True;
            this.ChartArea.AxisY.Name = "Rezervime";
            this.ChartArea.AxisY.Maximum = 10;
            this.ChartArea.AxisY.Minimum = 0;
            this.ChartArea.AxisY.LineColor = Colors.BLACK;
            this.ChartArea.AxisY.MajorGrid.LineColor = Colors.DOVE_GRAY;
            this.ChartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            this.ChartArea.BackColor = Colors.ALTO;

            this.ReservationsSeries = new Series();
            this.ReservationsSeries.BorderWidth = 2;
            this.ReservationsSeries.MarkerStyle = MarkerStyle.Circle;
            this.ReservationsSeries.MarkerSize = 10;
            this.ReservationsSeries.MarkerColor = Colors.POMEGRANATE;
            this.ReservationsSeries.ChartType = SeriesChartType.Line;

            this.DoctorReservationsChart = new Chart();
            this.DoctorReservationsChart.Name = "doctorReservationsChart";
            this.DoctorReservationsChart.ChartAreas.Add(this.ChartArea);
            this.DoctorReservationsChart.Series.Add(this.ReservationsSeries);
            this.DoctorReservationsChart.Size = new Size(
                chartWidth,
                bottomContainerHeight - 130
            );
            this.DoctorReservationsChart.Location = new Point(
                bottomContainerWidth - (Dimensions.PANEL_CARD_PADDING_HORIZONTAL + chartWidth),
                bottomContainerHeight - (Dimensions.PANEL_CARD_PADDING_HORIZONTAL + tableSize.Height)
            );
            this.bottomContainer.Controls.Add(this.DoctorReservationsChart);
            this.bottomContainer.Controls.SetChildIndex(this.DoctorReservationsChart, 0);
        }

        /**
         * Method to fetch necessary data
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
    }
}

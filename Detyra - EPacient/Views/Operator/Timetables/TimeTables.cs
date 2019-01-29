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
    class OperatorTimetables {
        public long SelectedWorkingHoursId { get; set; }
        public string SelectedEmployee { get; set; }
        public int SelectedEmployeeId { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Employee> Employees { get; set; }
        public OperatorEmployeesTable EmployeesTable { get; set; }
        public Label MondayStartLabel { get; set; }
        public Label MondayEndLabel { get; set; }
        public Label TuesdayStartLabel { get; set; }
        public Label TuesdayEndLabel { get; set; }
        public Label WednesdayStartLabel { get; set; }
        public Label WednesdayEndLabel { get; set; }
        public Label ThursdayStartLabel { get; set; }
        public Label ThursdayEndLabel { get; set; }
        public Label FridayStartLabel { get; set; }
        public Label FridayEndLabel { get; set; }
        public Label SaturdayStartLabel { get; set; }
        public Label SaturdayEndLabel { get; set; }
        public Label SundayStartLabel { get; set; }
        public Label SundayEndLabel { get; set; }
        public Label EmployeeLabelValue { get; set; }
        public TextBox SearchTermTxtBox { get; set; }

        private OperatorTimetablesController controller;
        private NavigationBar header;
        private Point tableLocation;
        private Size tableSize;
        private GroupBox right;
        private GroupBox left;
        private Label mondayLabel;
        private Label tuesdayLabel;
        private Label wednesdayLabel;
        private Label thursdayLabel;
        private Label fridayLabel;
        private Label saturdayLabel;
        private Label sundayLabel;
        private Label employeeLabel;
        private Label startTimeLabel;
        private Label endTimeLabel;
        private Label searchLabel;

        private int cardHeight;
        private int leftPanelWidth;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public OperatorTimetables(Panel previousPanel) {
            // Dimensions
            leftPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
            cardHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
            formComponentKeyWidth = (int) (0.3 * this.rightPanelWidth);
            formComponentValueWidth = (int) (0.3 * this.rightPanelWidth);
            formComponentHorizontalMargin = 27;

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init controller
            this.controller = new OperatorTimetablesController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "operatorTimetablesMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.PERSIAN_INDIGO,
                "Oraret e punës",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/operator.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Init left container
            left = new GroupBox();
            left.Text = "Lista e punonjësve të regjistruar";
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

            // Employees table
            Point tableLocation = new Point(Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 100);
            Size tableSize = new Size(
                this.leftPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.cardHeight - 110
            );

            this.tableLocation = tableLocation;
            this.tableSize = tableSize;

            this.EmployeesTable = new OperatorEmployeesTable(
                this.tableSize,
                this.tableLocation,
                this.Employees,
                this.controller
            );
            this.left.Controls.Add(this.EmployeesTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Oraret";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.rightPanelWidth),
                80
            );
            right.Size = new Size(this.rightPanelWidth, this.cardHeight);
            right.FlatStyle = FlatStyle.Flat;
            right.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Panel.Controls.Add(this.right);

            // Employee
            this.employeeLabel = new Label();
            this.employeeLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.employeeLabel.Width = this.formComponentKeyWidth;
            this.employeeLabel.Height = this.formComponentHeight;
            this.employeeLabel.Text = "Punonjësi";
            this.employeeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.employeeLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.employeeLabel);

            this.EmployeeLabelValue = new Label();
            this.EmployeeLabelValue.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.EmployeeLabelValue.Width = 2 * this.formComponentValueWidth;
            this.EmployeeLabelValue.Height = this.formComponentHeight;
            this.EmployeeLabelValue.Text = this.SelectedEmployee != null ? this.SelectedEmployee : "-";
            this.EmployeeLabelValue.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.EmployeeLabelValue.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.EmployeeLabelValue);

            // Start and end labels

            this.startTimeLabel = new Label();
            this.startTimeLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.startTimeLabel.Width = this.formComponentValueWidth;
            this.startTimeLabel.Height = this.formComponentHeight;
            this.startTimeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.startTimeLabel.ForeColor = Colors.DOVE_GRAY;
            this.startTimeLabel.Text = "Fillimi";
            this.right.Controls.Add(this.startTimeLabel);

            this.endTimeLabel = new Label();
            this.endTimeLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.endTimeLabel.Width = this.formComponentValueWidth;
            this.endTimeLabel.Height = this.formComponentHeight;
            this.endTimeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.endTimeLabel.Text = "Fundi";
            this.endTimeLabel.ForeColor = Colors.DOVE_GRAY;
            this.right.Controls.Add(this.endTimeLabel);

            /* Init week days form */

            // Monday
            this.mondayLabel = new Label();
            this.mondayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.mondayLabel.Width = this.formComponentKeyWidth;
            this.mondayLabel.Height = this.formComponentHeight;
            this.mondayLabel.Text = WeekDays.MONDAY;
            this.mondayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.mondayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.mondayLabel);

            this.MondayStartLabel = new Label();
            this.MondayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MondayStartLabel.Width = this.formComponentValueWidth;
            this.MondayStartLabel.Height = this.formComponentHeight;
            this.MondayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.MondayStartLabel);

            this.MondayEndLabel = new Label();
            this.MondayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MondayEndLabel.Width = this.formComponentValueWidth;
            this.MondayEndLabel.Height = this.formComponentHeight;
            this.MondayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.MondayEndLabel);

            // Tuesday
            this.tuesdayLabel = new Label();
            this.tuesdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.tuesdayLabel.Width = this.formComponentKeyWidth;
            this.tuesdayLabel.Height = this.formComponentHeight;
            this.tuesdayLabel.Text = WeekDays.TUESDAY;
            this.tuesdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.tuesdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.tuesdayLabel);

            this.TuesdayStartLabel = new Label();
            this.TuesdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.TuesdayStartLabel.Width = this.formComponentValueWidth;
            this.TuesdayStartLabel.Height = this.formComponentHeight;
            this.TuesdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.TuesdayStartLabel);

            this.TuesdayEndLabel = new Label();
            this.TuesdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.TuesdayEndLabel.Width = this.formComponentValueWidth;
            this.TuesdayEndLabel.Height = this.formComponentHeight;
            this.TuesdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.TuesdayEndLabel);

            // Wednesday
            this.wednesdayLabel = new Label();
            this.wednesdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.wednesdayLabel.Width = this.formComponentKeyWidth;
            this.wednesdayLabel.Height = this.formComponentHeight;
            this.wednesdayLabel.Text = WeekDays.WEDNESDAY;
            this.wednesdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.wednesdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.wednesdayLabel);

            this.WednesdayStartLabel = new Label();
            this.WednesdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.WednesdayStartLabel.Width = this.formComponentValueWidth;
            this.WednesdayStartLabel.Height = this.formComponentHeight;
            this.WednesdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.WednesdayStartLabel);

            this.WednesdayEndLabel = new Label();
            this.WednesdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.WednesdayEndLabel.Width = this.formComponentValueWidth;
            this.WednesdayEndLabel.Height = this.formComponentHeight;
            this.WednesdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.WednesdayEndLabel);

            // Thursday
            this.thursdayLabel = new Label();
            this.thursdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.thursdayLabel.Width = this.formComponentKeyWidth;
            this.thursdayLabel.Height = this.formComponentHeight;
            this.thursdayLabel.Text = WeekDays.THURSDAY;
            this.thursdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.thursdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.thursdayLabel);

            this.ThursdayStartLabel = new Label();
            this.ThursdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.ThursdayStartLabel.Width = this.formComponentValueWidth;
            this.ThursdayStartLabel.Height = this.formComponentHeight;
            this.ThursdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.ThursdayStartLabel);

            this.ThursdayEndLabel = new Label();
            this.ThursdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.ThursdayEndLabel.Width = this.formComponentValueWidth;
            this.ThursdayEndLabel.Height = this.formComponentHeight;
            this.ThursdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.ThursdayEndLabel);

            // Friday
            this.fridayLabel = new Label();
            this.fridayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.fridayLabel.Width = this.formComponentKeyWidth;
            this.fridayLabel.Height = this.formComponentHeight;
            this.fridayLabel.Text = WeekDays.FRIDAY;
            this.fridayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.fridayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.fridayLabel);

            this.FridayStartLabel = new Label();
            this.FridayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FridayStartLabel.Width = this.formComponentValueWidth;
            this.FridayStartLabel.Height = this.formComponentHeight;
            this.FridayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FridayStartLabel);

            this.FridayEndLabel = new Label();
            this.FridayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FridayEndLabel.Width = this.formComponentValueWidth;
            this.FridayEndLabel.Height = this.formComponentHeight;
            this.FridayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FridayEndLabel);

            // Saturday
            this.saturdayLabel = new Label();
            this.saturdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.saturdayLabel.Width = this.formComponentKeyWidth;
            this.saturdayLabel.Height = this.formComponentHeight;
            this.saturdayLabel.Text = WeekDays.SATURDAY;
            this.saturdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.saturdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.saturdayLabel);

            this.SaturdayStartLabel = new Label();
            this.SaturdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SaturdayStartLabel.Width = this.formComponentValueWidth;
            this.SaturdayStartLabel.Height = this.formComponentHeight;
            this.SaturdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.SaturdayStartLabel);

            this.SaturdayEndLabel = new Label();
            this.SaturdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SaturdayEndLabel.Width = this.formComponentValueWidth;
            this.SaturdayEndLabel.Height = this.formComponentHeight;
            this.SaturdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.SaturdayEndLabel);

            // Sunday
            this.sundayLabel = new Label();
            this.sundayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (9 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.sundayLabel.Width = this.formComponentKeyWidth;
            this.sundayLabel.Height = this.formComponentHeight;
            this.sundayLabel.Text = WeekDays.SUNDAY;
            this.sundayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.sundayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.sundayLabel);

            this.SundayStartLabel = new Label();
            this.SundayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (9 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SundayStartLabel.Width = this.formComponentValueWidth;
            this.SundayStartLabel.Height = this.formComponentHeight;
            this.SundayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.SundayStartLabel);

            this.SundayEndLabel = new Label();
            this.SundayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (9 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SundayEndLabel.Width = this.formComponentValueWidth;
            this.SundayEndLabel.Height = this.formComponentHeight;
            this.SundayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.SundayEndLabel);
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
    }
}

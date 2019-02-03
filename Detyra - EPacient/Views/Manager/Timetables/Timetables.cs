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
    class Timetables {
        public long SelectedWorkingHoursId { get; set; }
        public string SelectedEmployee { get; set; }
        public int SelectedEmployeeId { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Employee> Employees { get; set; }
        public DynamicEmployeesTable EmployeesTable { get; set; }
        public DateTimePicker MondayTimePickerStart { get; set; }
        public DateTimePicker MondayTimePickerEnd { get; set; }
        public DateTimePicker TuesdayTimePickerStart { get; set; }
        public DateTimePicker TuesdayTimePickerEnd { get; set; }
        public DateTimePicker WednesdayTimePickerStart { get; set; }
        public DateTimePicker WednesdayTimePickerEnd { get; set; }
        public DateTimePicker ThursdayTimePickerStart { get; set; }
        public DateTimePicker ThursdayTimePickerEnd { get; set; }
        public DateTimePicker FridayTimePickerStart { get; set; }
        public DateTimePicker FridayTimePickerEnd { get; set; }
        public DateTimePicker SaturdayTimePickerStart { get; set; }
        public DateTimePicker SaturdayTimePickerEnd { get; set; }
        public DateTimePicker SundayTimePickerStart { get; set; }
        public DateTimePicker SundayTimePickerEnd { get; set; }
        public Label EmployeeLabelValue { get; set; }

        private TimetablesController controller;
        private NavigationBar header;
        private Point tableLocation;
        private Size tableSize;
        private GroupBox right;
        private Label mondayLabel;
        private Label tuesdayLabel;
        private Label wednesdayLabel;
        private Label thursdayLabel;
        private Label fridayLabel;
        private Label saturdayLabel;
        private Label sundayLabel;
        private Button submitBtn;
        private Button resetBtn;
        private Label employeeLabel;

        private int tableWidth;
        private int tableHeight;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public Timetables(Panel previousPanel) {
            // Dimensions
            tableWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
            tableHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
            formComponentKeyWidth = (int) (0.3 * this.rightPanelWidth);
            formComponentValueWidth = (int) (0.3 * this.rightPanelWidth);
            formComponentHorizontalMargin = 27;

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init controller
            this.controller = new TimetablesController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "timetablesMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.BAHAMA_BLUE,
                "Menaxhimi i orareve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/manager.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Employees table
            this.tableLocation = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, 80);
            this.tableSize = new Size(
                this.tableWidth,
                this.tableHeight
            );

            this.EmployeesTable = new DynamicEmployeesTable(
                this.tableSize,
                this.tableLocation,
                this.Employees,
                this.controller
            );
            this.Panel.Controls.Add(this.EmployeesTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Përditësimi i orareve të punës";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.rightPanelWidth),
                80
            );
            right.Size = new Size(this.rightPanelWidth, this.tableHeight);
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

            /* Init week days form */

            // Monday
            this.mondayLabel = new Label();
            this.mondayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.mondayLabel.Width = this.formComponentKeyWidth;
            this.mondayLabel.Height = this.formComponentHeight;
            this.mondayLabel.Text = WeekDays.MONDAY;
            this.mondayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.mondayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.mondayLabel);

            this.MondayTimePickerStart = new DateTimePicker();
            this.MondayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MondayTimePickerStart.Width = this.formComponentValueWidth;
            this.MondayTimePickerStart.Height = this.formComponentHeight;
            this.MondayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.MondayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.MondayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.MondayTimePickerStart);

            this.MondayTimePickerEnd = new DateTimePicker();
            this.MondayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2* this.formComponentHorizontalMargin),
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MondayTimePickerEnd.Width = this.formComponentValueWidth;
            this.MondayTimePickerEnd.Height = this.formComponentHeight;
            this.MondayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.MondayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.MondayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.MondayTimePickerEnd);

            // Tuesday
            this.tuesdayLabel = new Label();
            this.tuesdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.tuesdayLabel.Width = this.formComponentKeyWidth;
            this.tuesdayLabel.Height = this.formComponentHeight;
            this.tuesdayLabel.Text = WeekDays.TUESDAY;
            this.tuesdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.tuesdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.tuesdayLabel);

            this.TuesdayTimePickerStart = new DateTimePicker();
            this.TuesdayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.TuesdayTimePickerStart.Width = this.formComponentValueWidth;
            this.TuesdayTimePickerStart.Height = this.formComponentHeight;
            this.TuesdayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.TuesdayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.TuesdayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.TuesdayTimePickerStart);

            this.TuesdayTimePickerEnd = new DateTimePicker();
            this.TuesdayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.TuesdayTimePickerEnd.Width = this.formComponentValueWidth;
            this.TuesdayTimePickerEnd.Height = this.formComponentHeight;
            this.TuesdayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.TuesdayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.TuesdayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.TuesdayTimePickerEnd);

            // Wednesday
            this.wednesdayLabel = new Label();
            this.wednesdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.wednesdayLabel.Width = this.formComponentKeyWidth;
            this.wednesdayLabel.Height = this.formComponentHeight;
            this.wednesdayLabel.Text = WeekDays.WEDNESDAY;
            this.wednesdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.wednesdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.wednesdayLabel);

            this.WednesdayTimePickerStart = new DateTimePicker();
            this.WednesdayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.WednesdayTimePickerStart.Width = this.formComponentValueWidth;
            this.WednesdayTimePickerStart.Height = this.formComponentHeight;
            this.WednesdayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.WednesdayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.WednesdayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.WednesdayTimePickerStart);

            this.WednesdayTimePickerEnd = new DateTimePicker();
            this.WednesdayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.WednesdayTimePickerEnd.Width = this.formComponentValueWidth;
            this.WednesdayTimePickerEnd.Height = this.formComponentHeight;
            this.WednesdayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.WednesdayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.WednesdayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.WednesdayTimePickerEnd);

            // Thursday
            this.thursdayLabel = new Label();
            this.thursdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.thursdayLabel.Width = this.formComponentKeyWidth;
            this.thursdayLabel.Height = this.formComponentHeight;
            this.thursdayLabel.Text = WeekDays.THURSDAY;
            this.thursdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.thursdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.thursdayLabel);

            this.ThursdayTimePickerStart = new DateTimePicker();
            this.ThursdayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.ThursdayTimePickerStart.Width = this.formComponentValueWidth;
            this.ThursdayTimePickerStart.Height = this.formComponentHeight;
            this.ThursdayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.ThursdayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.ThursdayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.ThursdayTimePickerStart);

            this.ThursdayTimePickerEnd = new DateTimePicker();
            this.ThursdayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.ThursdayTimePickerEnd.Width = this.formComponentValueWidth;
            this.ThursdayTimePickerEnd.Height = this.formComponentHeight;
            this.ThursdayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.ThursdayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.ThursdayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.ThursdayTimePickerEnd);

            // Friday
            this.fridayLabel = new Label();
            this.fridayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.fridayLabel.Width = this.formComponentKeyWidth;
            this.fridayLabel.Height = this.formComponentHeight;
            this.fridayLabel.Text = WeekDays.FRIDAY;
            this.fridayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.fridayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.fridayLabel);

            this.FridayTimePickerStart = new DateTimePicker();
            this.FridayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FridayTimePickerStart.Width = this.formComponentValueWidth;
            this.FridayTimePickerStart.Height = this.formComponentHeight;
            this.FridayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.FridayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.FridayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.FridayTimePickerStart);

            this.FridayTimePickerEnd = new DateTimePicker();
            this.FridayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FridayTimePickerEnd.Width = this.formComponentValueWidth;
            this.FridayTimePickerEnd.Height = this.formComponentHeight;
            this.FridayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.FridayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.FridayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.FridayTimePickerEnd);

            // Saturday
            this.saturdayLabel = new Label();
            this.saturdayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.saturdayLabel.Width = this.formComponentKeyWidth;
            this.saturdayLabel.Height = this.formComponentHeight;
            this.saturdayLabel.Text = WeekDays.SATURDAY;
            this.saturdayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.saturdayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.saturdayLabel);

            this.SaturdayTimePickerStart = new DateTimePicker();
            this.SaturdayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SaturdayTimePickerStart.Width = this.formComponentValueWidth;
            this.SaturdayTimePickerStart.Height = this.formComponentHeight;
            this.SaturdayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.SaturdayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.SaturdayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.SaturdayTimePickerStart);

            this.SaturdayTimePickerEnd = new DateTimePicker();
            this.SaturdayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SaturdayTimePickerEnd.Width = this.formComponentValueWidth;
            this.SaturdayTimePickerEnd.Height = this.formComponentHeight;
            this.SaturdayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.SaturdayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.SaturdayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.SaturdayTimePickerEnd);

            // Sunday
            this.sundayLabel = new Label();
            this.sundayLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.sundayLabel.Width = this.formComponentKeyWidth;
            this.sundayLabel.Height = this.formComponentHeight;
            this.sundayLabel.Text = WeekDays.SUNDAY;
            this.sundayLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.sundayLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.sundayLabel);

            this.SundayTimePickerStart = new DateTimePicker();
            this.SundayTimePickerStart.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SundayTimePickerStart.Width = this.formComponentValueWidth;
            this.SundayTimePickerStart.Height = this.formComponentHeight;
            this.SundayTimePickerStart.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.SundayTimePickerStart.Format = DateTimePickerFormat.Time;
            this.SundayTimePickerStart.ShowUpDown = true;
            this.right.Controls.Add(this.SundayTimePickerStart);

            this.SundayTimePickerEnd = new DateTimePicker();
            this.SundayTimePickerEnd.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SundayTimePickerEnd.Width = this.formComponentValueWidth;
            this.SundayTimePickerEnd.Height = this.formComponentHeight;
            this.SundayTimePickerEnd.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.SundayTimePickerEnd.Format = DateTimePickerFormat.Time;
            this.SundayTimePickerEnd.ShowUpDown = true;
            this.right.Controls.Add(this.SundayTimePickerEnd);

            /* Buttons */

            this.resetBtn = new Button();
            this.resetBtn.Size = new Size(this.formComponentValueWidth, this.formComponentHeight);
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
            this.submitBtn.Size = new Size(this.formComponentValueWidth, this.formComponentHeight);
            this.submitBtn.Location = new Point(
                this.rightPanelWidth - (this.formComponentValueWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
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

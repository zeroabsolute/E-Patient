using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Nurse;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Nurse {
    class Schedule {

        public long SelectedWorkingHoursId { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Nurse> Nurses { get; set; }
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
        public Label NurseLabelValue { get; set; }
        public TextBox SearchTermTxtBox { get; set; }

        private ScheduleController controller;
        private NavigationBar header;
        private GroupBox container;
        private Label mondayLabel;
        private Label tuesdayLabel;
        private Label wednesdayLabel;
        private Label thursdayLabel;
        private Label fridayLabel;
        private Label saturdayLabel;
        private Label sundayLabel;
        private Label nurseLabel;
        private Label startTimeLabel;
        private Label endTimeLabel;


        private int cardHeight;
        private int PanelWidth;
        private int formComponentVerticalMargin = 60;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public Schedule(Panel previousPanel) {
            // Dimensions       
            cardHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 70);
            PanelWidth = (int)(Dimensions.PANEL_WIDTH * 1) ;
            formComponentKeyWidth = (int)(0.3 * this.PanelWidth);
            formComponentValueWidth = (int)(0.3 * this.PanelWidth);
            formComponentHorizontalMargin = 50;

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init controller
            this.controller = new ScheduleController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "scheduleMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.IMPERIAL_RED,
                "Oraret",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/nurse.png"
            );
            this.Panel.Controls.Add(this.header.Panel);
 

            // Init container
            container = new GroupBox();
            container.Text = "Oraret";
            container.Location = new Point(
                (Dimensions.PANEL_WIDTH + 60) - (Dimensions.PANEL_PADDING_HORIZONTAL + this.PanelWidth ),
                90
            );
            container.Size = new Size(this.PanelWidth-80, this.cardHeight);
            container.FlatStyle = FlatStyle.Flat;
            container.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Panel.Controls.Add(this.container);

            // Employee
            this.nurseLabel = new Label();
            this.nurseLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.nurseLabel.Width = this.formComponentKeyWidth;
            this.nurseLabel.Height = this.formComponentHeight;
            this.nurseLabel.Text = "Infermier";
            this.nurseLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.nurseLabel.ForeColor = Colors.BLACK;
            this.container.Controls.Add(this.nurseLabel);

            this.NurseLabelValue = new Label();
            this.NurseLabelValue.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.NurseLabelValue.Width = 2 * this.formComponentValueWidth;
            this.NurseLabelValue.Height = this.formComponentHeight;
            this.NurseLabelValue.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.NurseLabelValue.ForeColor = Colors.BLACK;
            this.container.Controls.Add(this.NurseLabelValue);

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
            this.container.Controls.Add(this.startTimeLabel);

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
            this.container.Controls.Add(this.endTimeLabel);

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
            this.container.Controls.Add(this.mondayLabel);

            this.MondayStartLabel = new Label();
            this.MondayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MondayStartLabel.Width = this.formComponentValueWidth;
            this.MondayStartLabel.Height = this.formComponentHeight;
            this.MondayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.MondayStartLabel);

            this.MondayEndLabel = new Label();
            this.MondayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.MondayEndLabel.Width = this.formComponentValueWidth;
            this.MondayEndLabel.Height = this.formComponentHeight;
            this.MondayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.MondayEndLabel);

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
            this.container.Controls.Add(this.tuesdayLabel);

            this.TuesdayStartLabel = new Label();
            this.TuesdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.TuesdayStartLabel.Width = this.formComponentValueWidth;
            this.TuesdayStartLabel.Height = this.formComponentHeight;
            this.TuesdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.TuesdayStartLabel);

            this.TuesdayEndLabel = new Label();
            this.TuesdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.TuesdayEndLabel.Width = this.formComponentValueWidth;
            this.TuesdayEndLabel.Height = this.formComponentHeight;
            this.TuesdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.TuesdayEndLabel);

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
            this.container.Controls.Add(this.wednesdayLabel);

            this.WednesdayStartLabel = new Label();
            this.WednesdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.WednesdayStartLabel.Width = this.formComponentValueWidth;
            this.WednesdayStartLabel.Height = this.formComponentHeight;
            this.WednesdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.WednesdayStartLabel);

            this.WednesdayEndLabel = new Label();
            this.WednesdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (5 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.WednesdayEndLabel.Width = this.formComponentValueWidth;
            this.WednesdayEndLabel.Height = this.formComponentHeight;
            this.WednesdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.WednesdayEndLabel);

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
            this.container.Controls.Add(this.thursdayLabel);

            this.ThursdayStartLabel = new Label();
            this.ThursdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.ThursdayStartLabel.Width = this.formComponentValueWidth;
            this.ThursdayStartLabel.Height = this.formComponentHeight;
            this.ThursdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.ThursdayStartLabel);

            this.ThursdayEndLabel = new Label();
            this.ThursdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.ThursdayEndLabel.Width = this.formComponentValueWidth;
            this.ThursdayEndLabel.Height = this.formComponentHeight;
            this.ThursdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.ThursdayEndLabel);

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
            this.container.Controls.Add(this.fridayLabel);

            this.FridayStartLabel = new Label();
            this.FridayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FridayStartLabel.Width = this.formComponentValueWidth;
            this.FridayStartLabel.Height = this.formComponentHeight;
            this.FridayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.FridayStartLabel);

            this.FridayEndLabel = new Label();
            this.FridayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FridayEndLabel.Width = this.formComponentValueWidth;
            this.FridayEndLabel.Height = this.formComponentHeight;
            this.FridayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.FridayEndLabel);

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
            this.container.Controls.Add(this.saturdayLabel);

            this.SaturdayStartLabel = new Label();
            this.SaturdayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SaturdayStartLabel.Width = this.formComponentValueWidth;
            this.SaturdayStartLabel.Height = this.formComponentHeight;
            this.SaturdayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.SaturdayStartLabel);

            this.SaturdayEndLabel = new Label();
            this.SaturdayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (8 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SaturdayEndLabel.Width = this.formComponentValueWidth;
            this.SaturdayEndLabel.Height = this.formComponentHeight;
            this.SaturdayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.SaturdayEndLabel);

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
            this.container.Controls.Add(this.sundayLabel);

            this.SundayStartLabel = new Label();
            this.SundayStartLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (9 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SundayStartLabel.Width = this.formComponentValueWidth;
            this.SundayStartLabel.Height = this.formComponentHeight;
            this.SundayStartLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.SundayStartLabel);

            this.SundayEndLabel = new Label();
            this.SundayEndLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + (2 * this.formComponentKeyWidth) + (2 * this.formComponentHorizontalMargin),
                (9 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.SundayEndLabel.Width = this.formComponentValueWidth;
            this.SundayEndLabel.Height = this.formComponentHeight;
            this.SundayEndLabel.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.container.Controls.Add(this.SundayEndLabel);
        }

        /**
         * Method to initialize components and fetch necessary data
         */
        public void readInitialData() {
            this.controller.init();
        }
    }

}

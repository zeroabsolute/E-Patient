using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Nurse;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;

namespace Detyra___EPacient.Views.Nurse {
    class NurseMainPanel {
        public User LoggedInUser { get; set; }
        public Panel Panel { get; set; }
        public Button Reservations { get; set; }
        public Button AnalysisNurse { get; set; }
        public Button Schedule { get; set; }
        public Button LogOut { get; set; }

        public Schedule SchedulePanel { get; set; }
        public AnalysisNurse AnalysisPanel { get; set; }
        public ReservationsNurse ReservationsNursePanel { get; set; }
        public LogInPanel LogInPanel { get; set; }

        public const string SCHEDULE_BTN = "nurseScheduleBtn";
        public const string RESERVATIONS_BTN = "nurseReservationsBtn";
        public const string ANALYSIS_BTN = "nurseAnalysisBtn";
        public const string LOG_OUT_BTN = "nurseLogOutBtn";

        private PictureBox avatar;
        private TableLayoutPanel menuContainer = null;
        private NurseMainController controller;

        /* Constructor */

        public NurseMainPanel() {
            controller = new NurseMainController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "nurseMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init picture box
            this.avatar = new PictureBox();
            this.avatar.Location = Dimensions.AVATAR_LOCATION;
            this.avatar.Name = "avatar";
            this.avatar.Size = new Size(Dimensions.AVATAR_SIZE, Dimensions.AVATAR_SIZE);
            this.avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.avatar.ImageLocation = "../../Resources/nurse.png";

            this.Panel.Controls.Add(this.avatar);

            // Init menu container
            this.menuContainer = new TableLayoutPanel();
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Location = Dimensions.MENU_LOCATION;
            this.menuContainer.Size = new Size(Dimensions.MENU_CONTAINER_WIDTH, Dimensions.MENU_CONTAINER_HEIGHT);

            this.Panel.Controls.Add(this.menuContainer);

            /* Init menu buttons */

            int buttonHeight = (int)Dimensions.MENU_CONTAINER_HEIGHT / 3;
            int bigButtonWidth = (int)(Dimensions.MENU_CONTAINER_WIDTH);
            int smallButtonWidth = (int) (Dimensions.MENU_CONTAINER_WIDTH * 0.5);

            // Schedule
            this.Schedule = new Button();
            this.Schedule.Name = SCHEDULE_BTN;
            this.Schedule.Text = "Oraret";
            this.Schedule.Size = new Size(bigButtonWidth, buttonHeight);
            this.Schedule.Image = Image.FromFile("../../Resources/clock.png");
            this.Schedule.ImageAlign = ContentAlignment.BottomCenter;
            this.Schedule.TextAlign = ContentAlignment.MiddleCenter;
            this.Schedule.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Schedule.UseVisualStyleBackColor = true;
            this.Schedule.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Schedule.ForeColor = Colors.WHITE;
            this.Schedule.BackColor = Colors.SUNGLO;
            this.Schedule.FlatStyle = FlatStyle.Flat;
            this.Schedule.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Schedule);
            this.menuContainer.SetRow(this.Schedule, 0);
            this.menuContainer.SetColumn(this.Schedule, 0);
            this.menuContainer.SetColumnSpan(this.Schedule, 2);

            // Reservations
            this.Reservations = new Button();
            this.Reservations.Name = RESERVATIONS_BTN;
            this.Reservations.Text = "Rezervimet";
            this.Reservations.Size = new Size(smallButtonWidth, buttonHeight);
            this.Reservations.Image = Image.FromFile("../../Resources/calendar.png");
            this.Reservations.ImageAlign = ContentAlignment.BottomCenter;
            this.Reservations.TextAlign = ContentAlignment.MiddleCenter;
            this.Reservations.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Reservations.UseVisualStyleBackColor = true;
            this.Reservations.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Reservations.ForeColor = Colors.WHITE;
            this.Reservations.BackColor = Colors.SALMON_RED;
            this.Reservations.FlatStyle = FlatStyle.Flat;
            this.Reservations.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Reservations);
            this.menuContainer.SetRow(this.Reservations, 1);
            this.menuContainer.SetColumn(this.Reservations, 0);

            // Analysis
            this.AnalysisNurse = new Button();
            this.AnalysisNurse.Name = ANALYSIS_BTN;
            this.AnalysisNurse.Text = "Analizat";
            this.AnalysisNurse.Size = new Size(smallButtonWidth, buttonHeight);
            this.AnalysisNurse.Image = Image.FromFile("../../Resources/description.png");
            this.AnalysisNurse.ImageAlign = ContentAlignment.BottomCenter;
            this.AnalysisNurse.TextAlign = ContentAlignment.MiddleCenter;
            this.AnalysisNurse.TextImageRelation = TextImageRelation.ImageAboveText;
            this.AnalysisNurse.UseVisualStyleBackColor = true;
            this.AnalysisNurse.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.AnalysisNurse.ForeColor = Colors.WHITE;
            this.AnalysisNurse.BackColor = Colors.GUARDSMAN_RED;
            this.AnalysisNurse.FlatStyle = FlatStyle.Flat;
            this.AnalysisNurse.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.AnalysisNurse);
            this.menuContainer.SetRow(this.AnalysisNurse, 1);
            this.menuContainer.SetColumn(this.AnalysisNurse, 1);

            // Log out
            this.LogOut = new Button();
            this.LogOut.Name = LOG_OUT_BTN;
            this.LogOut.Text = "Dil";
            this.LogOut.Size = new Size(bigButtonWidth, buttonHeight);
            this.LogOut.Image = Image.FromFile("../../Resources/log-out.png");
            this.LogOut.ImageAlign = ContentAlignment.BottomCenter;
            this.LogOut.TextAlign = ContentAlignment.MiddleCenter;
            this.LogOut.TextImageRelation = TextImageRelation.ImageAboveText;
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.LogOut.ForeColor = Colors.WHITE;
            this.LogOut.BackColor = Colors.IMPERIAL_RED;
            this.LogOut.FlatStyle = FlatStyle.Flat;
            this.LogOut.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.LogOut);
            this.menuContainer.SetRow(this.LogOut, 2);
            this.menuContainer.SetColumn(this.LogOut, 0);
            this.menuContainer.SetColumnSpan(this.LogOut, 2);
        }

        public void initNextPanels(
           Schedule s,    
           LogInPanel l,
           ReservationsNurse r,
           AnalysisNurse a
       ) {
            this.SchedulePanel = s;
            this.LogInPanel = l;
            this.AnalysisPanel = a;
            this.ReservationsNursePanel = r;
        }

        /* Event handlers */

        private void onMenuButtonClicked(object sender, EventArgs e) {
            Button s = (Button)sender;
            this.controller.handleMenuButtonClick(s.Name);
        }
    }
}

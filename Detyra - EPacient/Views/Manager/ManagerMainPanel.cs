using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Manager;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Views.Manager {
    class ManagerMainPanel {
        public User LoggedInUser { get; set; }

        public Panel Panel { get; set; }
        public Button Users { get; set; }
        public Button Timetables { get; set; }
        public Button DoctorInCharge { get; set; }
        public Button Services { get; set; }
        public Button Medicaments { get; set; }
        public Button Analytics { get; set; }
        public Button LogOut { get; set; }

        public Users UsersPanel { get; set; }
        public Timetables TimetablesPanel { get; set; }
        public Services ServicesPanel { get; set; }
        public Medicaments MedicamentsPanel { get; set; }
        public DoctorInCharge DoctorInChargePanel { get; set; }
        public ManagerAnalytics AnalyticsPanel { get; set; }
        public LogInPanel LogInPanel { get; set; }

        public const string USERS_BTN = "managerUsersBtn";
        public const string TIMETABLES_BTN = "managerTimetablesBtn";
        public const string DOCTOR_IN_CHARGE_BTN = "managerDoctorInChargeBtn";
        public const string SERVICES_BTN = "managerServicesBtn";
        public const string MEDICAMENTS_BTN = "managerMedicamentsBtn";
        public const string ANALYTICS_BTN = "managerAnalyticsBtn";
        public const string LOG_OUT_BTN = "managerLogOutBtn";

        private PictureBox avatar;
        private TableLayoutPanel menuContainer = null;
        private ManagerMainController controller;

        /* Constructor */

        public ManagerMainPanel() {
            controller = new ManagerMainController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "managerMainPanel";
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
            this.avatar.ImageLocation = "../../Resources/manager.png";

            this.Panel.Controls.Add(this.avatar);

            // Init menu container
            this.menuContainer = new TableLayoutPanel();
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Location = Dimensions.MENU_LOCATION;
            this.menuContainer.Size = new Size(Dimensions.MENU_CONTAINER_WIDTH, Dimensions.MENU_CONTAINER_HEIGHT);

            this.Panel.Controls.Add(this.menuContainer);

            /* Init menu buttons */

            int buttonHeight = (int) Dimensions.MENU_CONTAINER_HEIGHT / 3;
            int bigButtonWidth = (int) (Dimensions.MENU_CONTAINER_WIDTH * 0.66);
            int smallButtonWidth = (int) (Dimensions.MENU_CONTAINER_WIDTH * 0.33);

            // Users
            this.Users = new Button();
            this.Users.Name = USERS_BTN;
            this.Users.Text = "Përdoruesit";
            this.Users.Size = new Size(smallButtonWidth, buttonHeight);
            this.Users.Image = Image.FromFile("../../Resources/users.png");
            this.Users.ImageAlign = ContentAlignment.MiddleCenter;
            this.Users.TextAlign = ContentAlignment.MiddleCenter;
            this.Users.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Users.UseVisualStyleBackColor = true;
            this.Users.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Users.ForeColor = Colors.WHITE;
            this.Users.BackColor = Colors.DENIM;
            this.Users.FlatStyle = FlatStyle.Flat;
            this.Users.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Users);
            this.menuContainer.SetRow(this.Users, 0);
            this.menuContainer.SetColumn(this.Users, 0);

            // Timetables
            this.Timetables = new Button();
            this.Timetables.Name = TIMETABLES_BTN;
            this.Timetables.Text = "Oraret";
            this.Timetables.Size = new Size(bigButtonWidth, buttonHeight);
            this.Timetables.Image = Image.FromFile("../../Resources/clock.png");
            this.Timetables.ImageAlign = ContentAlignment.MiddleCenter;
            this.Timetables.TextAlign = ContentAlignment.MiddleCenter;
            this.Timetables.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Timetables.UseVisualStyleBackColor = true;
            this.Timetables.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Timetables.ForeColor = Colors.WHITE;
            this.Timetables.BackColor = Colors.MALIBU;
            this.Timetables.FlatStyle = FlatStyle.Flat;
            this.Timetables.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Timetables);
            this.menuContainer.SetRow(this.Timetables, 0);
            this.menuContainer.SetColumn(this.Timetables, 1);
            this.menuContainer.SetColumnSpan(this.Timetables, 2);

            // Doctors in charge
            this.DoctorInCharge = new Button();
            this.DoctorInCharge.Name = DOCTOR_IN_CHARGE_BTN;
            this.DoctorInCharge.Text = "Mjekët Roje";
            this.DoctorInCharge.Size = new Size(smallButtonWidth, buttonHeight);
            this.DoctorInCharge.Image = Image.FromFile("../../Resources/calendar.png");
            this.DoctorInCharge.ImageAlign = ContentAlignment.MiddleCenter;
            this.DoctorInCharge.TextAlign = ContentAlignment.MiddleCenter;
            this.DoctorInCharge.TextImageRelation = TextImageRelation.ImageAboveText;
            this.DoctorInCharge.UseVisualStyleBackColor = true;
            this.DoctorInCharge.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.DoctorInCharge.ForeColor = Colors.WHITE;
            this.DoctorInCharge.BackColor = Colors.DODGER_BLUE;
            this.DoctorInCharge.FlatStyle = FlatStyle.Flat;
            this.DoctorInCharge.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.DoctorInCharge);
            this.menuContainer.SetRow(this.DoctorInCharge, 1);
            this.menuContainer.SetColumn(this.DoctorInCharge, 0);

            // Services
            this.Services = new Button();
            this.Services.Name = SERVICES_BTN;
            this.Services.Text = "Shërbimet";
            this.Services.Size = new Size(smallButtonWidth, buttonHeight);
            this.Services.Image = Image.FromFile("../../Resources/assignment.png");
            this.Services.ImageAlign = ContentAlignment.MiddleCenter;
            this.Services.TextAlign = ContentAlignment.MiddleCenter;
            this.Services.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Services.UseVisualStyleBackColor = true;
            this.Services.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Services.ForeColor = Colors.WHITE;
            this.Services.BackColor = Colors.BAHAMA_BLUE;
            this.Services.FlatStyle = FlatStyle.Flat;
            this.Services.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Services);
            this.menuContainer.SetRow(this.Services, 1);
            this.menuContainer.SetColumn(this.Services, 1);

            // Medicaments
            this.Medicaments = new Button();
            this.Medicaments.Name = MEDICAMENTS_BTN;
            this.Medicaments.Text = "Medikamentet";
            this.Medicaments.Size = new Size(smallButtonWidth, buttonHeight);
            this.Medicaments.Image = Image.FromFile("../../Resources/hospital.png");
            this.Medicaments.ImageAlign = ContentAlignment.MiddleCenter;
            this.Medicaments.TextAlign = ContentAlignment.MiddleCenter;
            this.Medicaments.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Medicaments.UseVisualStyleBackColor = true;
            this.Medicaments.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Medicaments.ForeColor = Colors.WHITE;
            this.Medicaments.BackColor = Colors.BLUE_LAGOON;
            this.Medicaments.FlatStyle = FlatStyle.Flat;
            this.Medicaments.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Medicaments);
            this.menuContainer.SetRow(this.Medicaments, 1);
            this.menuContainer.SetColumn(this.Medicaments, 2);

            // Analytics
            this.Analytics = new Button();
            this.Analytics.Name = ANALYTICS_BTN;
            this.Analytics.Text = "Statistikat";
            this.Analytics.Size = new Size(bigButtonWidth, buttonHeight);
            this.Analytics.Image = Image.FromFile("../../Resources/pie-chart.png");
            this.Analytics.ImageAlign = ContentAlignment.MiddleCenter;
            this.Analytics.TextAlign = ContentAlignment.MiddleCenter;
            this.Analytics.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Analytics.UseVisualStyleBackColor = true;
            this.Analytics.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Analytics.ForeColor = Colors.WHITE;
            this.Analytics.BackColor = Colors.CERULEAN;
            this.Analytics.FlatStyle = FlatStyle.Flat;
            this.Analytics.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Analytics);
            this.menuContainer.SetRow(this.Analytics, 2);
            this.menuContainer.SetColumn(this.Analytics, 0);
            this.menuContainer.SetColumnSpan(this.Analytics, 2);

            // Log out
            this.LogOut = new Button();
            this.LogOut.Name = LOG_OUT_BTN;
            this.LogOut.Text = "Dil";
            this.LogOut.Size = new Size(smallButtonWidth, buttonHeight);
            this.LogOut.Image = Image.FromFile("../../Resources/log-out.png");
            this.LogOut.ImageAlign = ContentAlignment.MiddleCenter;
            this.LogOut.TextAlign = ContentAlignment.MiddleCenter;
            this.LogOut.TextImageRelation = TextImageRelation.ImageAboveText;
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.LogOut.ForeColor = Colors.WHITE;
            this.LogOut.BackColor = Colors.BLUE_RIBBON;
            this.LogOut.FlatStyle = FlatStyle.Flat;
            this.LogOut.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.LogOut);
            this.menuContainer.SetRow(this.LogOut, 2);
            this.menuContainer.SetColumn(this.LogOut, 1);
        }

        /* Setters and getters */

        public void initNextPanels(
            Users u,
            Timetables t,
            Services s,
            Medicaments m,
            DoctorInCharge d,
            ManagerAnalytics a,
            LogInPanel l
        ) {
            this.UsersPanel = u;
            this.TimetablesPanel = t;
            this.ServicesPanel = s;
            this.MedicamentsPanel = m;
            this.DoctorInChargePanel = d;
            this.AnalyticsPanel = a;
            this.LogInPanel = l;
        }

        /* Event handlers */

        private void onMenuButtonClicked(object sender, EventArgs e) {
            Button s = (Button) sender;
            controller.handleMenuButtonClick(s.Name);
        }
    }
}

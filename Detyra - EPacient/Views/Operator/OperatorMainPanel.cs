using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Operator;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Views.Operator {
    class OperatorMainPanel {
        public User LoggedInUser { get; set; }
        public Panel Panel { get; set; }
        public Button Patients { get; set; }
        public Button PatientCharts { get; set; }
        public Button Reservations { get; set; }
        public Button TimeTables { get; set; }
        public Button LogOut { get; set; }

        public Patients PatientsPanel { get; set; }
        public OperatorPatientCharts PatientChartsPanel { get; set; }
        public Reservations ReservationsPanel { get; set; }
        public OperatorTimetables TimeTablesPanel { get; set; }
        public LogInPanel LogInPanel { get; set; }

        public const string PATIENTS_BTN = "operatorPatientsBtn";
        public const string PATIENT_CHARTS_BTN = "operatorPatientChartsBtn";
        public const string RESERVATIONS_BTN = "operatorReservationsBtn";
        public const string TIME_TABLES_BTN = "operatorTimeTablesBtn";
        public const string LOG_OUT_BTN = "operatorLogOutBtn";

        private PictureBox avatar;
        private TableLayoutPanel menuContainer = null;
        private OperatorMainController controller;

        /* Constructor */

        public OperatorMainPanel() {
            controller = new OperatorMainController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "operatorMainPanel";
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
            this.avatar.ImageLocation = "../../Resources/operator.png";

            this.Panel.Controls.Add(this.avatar);

            // Init menu container
            this.menuContainer = new TableLayoutPanel();
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Location = Dimensions.MENU_LOCATION;
            this.menuContainer.Size = new Size(Dimensions.MENU_CONTAINER_WIDTH, Dimensions.MENU_CONTAINER_HEIGHT);

            this.Panel.Controls.Add(this.menuContainer);

            /* Init menu buttons */

            int buttonHeight = (int)Dimensions.MENU_CONTAINER_HEIGHT / 3;
            int bigButtonWidth = (int)(Dimensions.MENU_CONTAINER_WIDTH );
            int smallButtonWidth = (int)(Dimensions.MENU_CONTAINER_WIDTH * 0.5);

            // Patients
            this.Patients = new Button();
            this.Patients.Name = PATIENTS_BTN;
            this.Patients.Text = "Pacientët";
            this.Patients.Size = new Size(smallButtonWidth, buttonHeight);
            this.Patients.Image = Image.FromFile("../../Resources/user.png");
            this.Patients.ImageAlign = ContentAlignment.MiddleCenter;
            this.Patients.TextAlign = ContentAlignment.MiddleCenter;
            this.Patients.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Patients.UseVisualStyleBackColor = true;
            this.Patients.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Patients.ForeColor = Colors.WHITE;
            this.Patients.BackColor = Colors.AMETHYST;
            this.Patients.FlatStyle = FlatStyle.Flat;
            this.Patients.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Patients);
            this.menuContainer.SetRow(this.Patients, 0);
            this.menuContainer.SetColumn(this.Patients, 0);

            // Patient charts
            this.PatientCharts = new Button();
            this.PatientCharts.Name = PATIENT_CHARTS_BTN;
            this.PatientCharts.Text = "Kartelat";
            this.PatientCharts.Size = new Size(smallButtonWidth, buttonHeight);
            this.PatientCharts.Image = Image.FromFile("../../Resources/description.png");
            this.PatientCharts.ImageAlign = ContentAlignment.MiddleCenter;
            this.PatientCharts.TextAlign = ContentAlignment.MiddleCenter;
            this.PatientCharts.TextImageRelation = TextImageRelation.ImageAboveText;
            this.PatientCharts.UseVisualStyleBackColor = true;
            this.PatientCharts.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.PatientCharts.ForeColor = Colors.WHITE;
            this.PatientCharts.BackColor = Colors.HELIOTROPE;
            this.PatientCharts.FlatStyle = FlatStyle.Flat;
            this.PatientCharts.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.PatientCharts);
            this.menuContainer.SetRow(this.PatientCharts, 0);
            this.menuContainer.SetColumn(this.PatientCharts, 1);
           
            // Reservations
            this.Reservations = new Button();
            this.Reservations.Name = RESERVATIONS_BTN;
            this.Reservations.Text = "Rezervimet";
            this.Reservations.Size = new Size(bigButtonWidth, buttonHeight);
            this.Reservations.Image = Image.FromFile("../../Resources/calendar.png");
            this.Reservations.ImageAlign = ContentAlignment.MiddleCenter;
            this.Reservations.TextAlign = ContentAlignment.MiddleCenter;
            this.Reservations.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Reservations.UseVisualStyleBackColor = true;
            this.Reservations.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Reservations.ForeColor = Colors.WHITE;
            this.Reservations.BackColor = Colors.PERSIAN_INDIGO;
            this.Reservations.FlatStyle = FlatStyle.Flat;
            this.Reservations.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Reservations);
            this.menuContainer.SetRow(this.Reservations, 1);
            this.menuContainer.SetColumn(this.Reservations, 0);
            this.menuContainer.SetColumnSpan(this.Reservations, 2);

            // Timetables
            this.TimeTables = new Button();
            this.TimeTables.Name = TIME_TABLES_BTN;
            this.TimeTables.Text = "Oraret";
            this.TimeTables.Size = new Size(smallButtonWidth, buttonHeight);
            this.TimeTables.Image = Image.FromFile("../../Resources/clock.png");
            this.TimeTables.ImageAlign = ContentAlignment.MiddleCenter;
            this.TimeTables.TextAlign = ContentAlignment.MiddleCenter;
            this.TimeTables.TextImageRelation = TextImageRelation.ImageAboveText;
            this.TimeTables.UseVisualStyleBackColor = true;
            this.TimeTables.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.TimeTables.ForeColor = Colors.WHITE;
            this.TimeTables.BackColor = Colors.JACKSONS_PURPLE;
            this.TimeTables.FlatStyle = FlatStyle.Flat;
            this.TimeTables.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.TimeTables);
            this.menuContainer.SetRow(this.TimeTables, 2);
            this.menuContainer.SetColumn(this.TimeTables, 0);

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
            this.LogOut.BackColor = Colors.LILAC_BUSH;
            this.LogOut.FlatStyle = FlatStyle.Flat;
            this.LogOut.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.LogOut);
            this.menuContainer.SetRow(this.LogOut, 2);
            this.menuContainer.SetColumn(this.LogOut, 1);
        }

        public void initNextPanels(
            Patients p,
            OperatorPatientCharts pc,
            Reservations r,
            OperatorTimetables t,
            LogInPanel l
        ) {
            this.PatientsPanel = p;
            this.PatientChartsPanel = pc;
            this.ReservationsPanel = r;
            this.TimeTablesPanel = t;
            this.LogInPanel = l;
        }

        /* Event handlers */

        private void onMenuButtonClicked(object sender, EventArgs e) {
            Button s = (Button)sender;
            controller.handleMenuButtonClick(s.Name);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Doctor;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Doctor;

namespace Detyra___EPacient.Views.Doctor {
    class DoctorMainPanel
    {
        public User LoggedInUser { get; set; }

        public Panel Panel { get; set; }
        public Button TimeTablesDoc { get; set; }
        public Button ReservationsDoc { get; set; }
        public Button Prescription { get; set; }
        public Button LogOut { get; set; }

        public TimeTablesDoc TimeTablesDocPanel { get; set; }
        public ReservationsDoc ReservationsDocPanel { get; set; }
        public Prescription PrescriptionPanel { get; set; }
        public LogInPanel LogInPanel { get; set; }

        public const string TIME_TABLES_DOC_BTN = "doctorTimeTablesDocBtn";
        public const string RESERVATIONS_DOC_BTN = "doctorReservationsDocBtn";
        public const string PRESCRIPTION_BTN = "doctorPrescriptionBtn";
        public const string LOG_OUT_BTN = "doctorLogOutBtn";

        private PictureBox avatar;
        private TableLayoutPanel menuContainer = null;
        private DoctorMainController controller;

        /* Constructor */

        public DoctorMainPanel() {
            controller = new DoctorMainController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "doctorMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Color.White;
            this.Panel.Visible = false;

            // Init picture box
            this.avatar = new PictureBox();
            this.avatar.Location = Dimensions.AVATAR_LOCATION;
            this.avatar.Name = "avatar";
            this.avatar.Size = new Size(Dimensions.AVATAR_SIZE, Dimensions.AVATAR_SIZE);
            this.avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.avatar.ImageLocation = "../../Resources/surgeon.png";

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
            int smallButtonWidth = (int)(Dimensions.MENU_CONTAINER_WIDTH * 0.5);

            // Timetables
            this.TimeTablesDoc = new Button();
            this.TimeTablesDoc.Name = TIME_TABLES_DOC_BTN;
            this.TimeTablesDoc.Text = "Oraret";
            this.TimeTablesDoc.Size = new Size(bigButtonWidth, buttonHeight);
            this.TimeTablesDoc.Image = Image.FromFile("../../Resources/clock.png");
            this.TimeTablesDoc.ImageAlign = ContentAlignment.BottomCenter;
            this.TimeTablesDoc.TextAlign = ContentAlignment.MiddleCenter;
            this.TimeTablesDoc.TextImageRelation = TextImageRelation.ImageAboveText;
            this.TimeTablesDoc.UseVisualStyleBackColor = true;
            this.TimeTablesDoc.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.TimeTablesDoc.ForeColor = Colors.WHITE;
            this.TimeTablesDoc.BackColor = Colors.MINT;
            this.TimeTablesDoc.FlatStyle = FlatStyle.Flat;
            this.TimeTablesDoc.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.TimeTablesDoc);
            this.menuContainer.SetRow(this.TimeTablesDoc, 0);
            this.menuContainer.SetColumn(this.TimeTablesDoc, 0);
            this.menuContainer.SetColumnSpan(this.TimeTablesDoc, 2);

            // Reservations
            this.ReservationsDoc = new Button();
            this.ReservationsDoc.Name = RESERVATIONS_DOC_BTN;
            this.ReservationsDoc.Text = "Rezervimet";
            this.ReservationsDoc.Size = new Size(smallButtonWidth, buttonHeight);
            this.ReservationsDoc.Image = Image.FromFile("../../Resources/calendar.png");
            this.ReservationsDoc.ImageAlign = ContentAlignment.BottomCenter;
            this.ReservationsDoc.TextAlign = ContentAlignment.MiddleCenter;
            this.ReservationsDoc.TextImageRelation = TextImageRelation.ImageAboveText;
            this.ReservationsDoc.UseVisualStyleBackColor = true;
            this.ReservationsDoc.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.ReservationsDoc.ForeColor = Colors.WHITE;
            this.ReservationsDoc.BackColor = Colors.PEA;
            this.ReservationsDoc.FlatStyle = FlatStyle.Flat;
            this.ReservationsDoc.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.ReservationsDoc);
            this.menuContainer.SetRow(this.ReservationsDoc, 1);
            this.menuContainer.SetColumn(this.ReservationsDoc, 0);

            // Prescriptions
            this.Prescription = new Button();
            this.Prescription.Name = PRESCRIPTION_BTN;
            this.Prescription.Text = "Receta";
            this.Prescription.Size = new Size(smallButtonWidth, buttonHeight);
            this.Prescription.Image = Image.FromFile("../../Resources/description.png");
            this.Prescription.ImageAlign = ContentAlignment.BottomCenter;
            this.Prescription.TextAlign = ContentAlignment.MiddleCenter;
            this.Prescription.TextImageRelation = TextImageRelation.ImageAboveText;
            this.Prescription.UseVisualStyleBackColor = true;
            this.Prescription.Font = new Font(Fonts.primary, 18, FontStyle.Bold);
            this.Prescription.ForeColor = Colors.WHITE;
            this.Prescription.BackColor = Colors.PEPER_GREEN;
            this.Prescription.FlatStyle = FlatStyle.Flat;
            this.Prescription.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.Prescription);
            this.menuContainer.SetRow(this.Prescription, 1);
            this.menuContainer.SetColumn(this.Prescription, 1);

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
            this.LogOut.BackColor = Colors.FOREST;
            this.LogOut.FlatStyle = FlatStyle.Flat;
            this.LogOut.Click += new EventHandler(onMenuButtonClicked);

            this.menuContainer.Controls.Add(this.LogOut);
            this.menuContainer.SetRow(this.LogOut, 2);
            this.menuContainer.SetColumn(this.LogOut, 0);
            this.menuContainer.SetColumnSpan(this.LogOut, 2);
        }

        /* Setters and getters */

        public void initNextPanels(
            TimeTablesDoc t,
            ReservationsDoc r,
            Prescription p,
            LogInPanel l
        ) {
            this.TimeTablesDocPanel = t;
            this.ReservationsDocPanel = r;
            this.PrescriptionPanel = p;
            this.LogInPanel = l;
        }

        /* Event handlers */

        private void onMenuButtonClicked(object sender, EventArgs e) {
            Button s = (Button) sender;
            controller.handleMenuButtonClick(s.Name);
        }
    }
}

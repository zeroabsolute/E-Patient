using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Manager;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Views {
    class OperatorMainPanel
    {
        
            public User LoggedInUser { get; set; }
            public Panel Panel { get; set; }
            public Button Patients { get; set; }
            public Button Reservations { get; set; }
            public Button TimeTables { get; set; }
            public Button LogOut { get; set; }

            public Patients PatientsPanel { get; set; }
            public Reservations ReservationsPanel { get; set; }
            public TimeTables TimeTablesPanel { get; set; }
            public LogInPanel LogInPanel { get; set; }

            public const string PATIENTS_BTN = "operatorPatientsBtn";
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
            this.Panel.BackColor = Color.CornflowerBlue;
            this.Panel.Visible = true;
            // Init picture box
            this.avatar = new PictureBox();
            this.avatar.Location = Dimensions.AVATAR_LOCATION;
            this.avatar.Name = "avatar";
            this.avatar.Size = new Size(Dimensions.AVATAR_SIZE, Dimensions.AVATAR_SIZE);
            this.avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.avatar.ImageLocation = "../../Resources/operator.png";

            this.Panel.Controls.Add(this.avatar);

        }
    }
}

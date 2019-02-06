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
using Detyra___EPacient.Views.Manager.Analytics;

namespace Detyra___EPacient.Views.Manager {
    class ManagerAnalytics {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public Card ServicesCard { get; set; }
        public Card PatientsCard { get; set; }
        public Card DoctorsCard { get; set; }
        public Card NursesCard { get; set; }

        private AnalyticsController controller;
        private NavigationBar header;

        public ManagerAnalytics(Panel previousPanel) {
            this.controller = new AnalyticsController(this);

            // Dimensions
            int occupiedSpace = (4 * Dimensions.TOP_CARD_SIZE.Width) + 2 * Dimensions.PANEL_PADDING_HORIZONTAL;
            int cardMargin = (int) (Dimensions.PANEL_WIDTH - occupiedSpace) / 3;

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
            this.Panel.Visible = true;

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
        }

        /**
         * Method to fetch necessary data
         */

        public async void readInitialData() {
            this.controller.init();
        }
    }
}

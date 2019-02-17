using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Doctor;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Doctor {
    class ReservationsDoc {
        public User LoggedInUser { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public DocReservationsTable ReservationsTable { get; set; }

        private NavigationBar header;
        private ReservationsController controller;

        public ReservationsDoc(Panel previousPanel) {
            this.controller = new ReservationsController(this);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "reservationsdocMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.FOREST,
                "Rezervimet",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/surgeon.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Reservations table
            Point tableLocation = new Point(
                Dimensions.PANEL_PADDING_HORIZONTAL, 
                Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL
            );
            Size tableSize = new Size(
                Dimensions.PANEL_WIDTH - (2 * Dimensions.PANEL_PADDING_HORIZONTAL),
                Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 2 * Dimensions.PANEL_PADDING_HORIZONTAL)
            );

            this.ReservationsTable = new DocReservationsTable(
                tableSize,
                tableLocation,
                this.controller
            );
            this.Panel.Controls.Add(this.ReservationsTable.DataGrid);
        }

        /**
         * Method to initialize components and fetch necessary data
         */

        public void readInitialData() {
            this.controller.init();
        }
    }
}

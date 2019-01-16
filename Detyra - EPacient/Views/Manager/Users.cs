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
    class Users {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public DynamicComboBox CBox;

        private NavigationBar header;
        private UsersController controller;

        public Users(Panel previousPanel) {
            controller = new UsersController(this);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "usersMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.BAHAMA_BLUE,
                "Menaxhimi i përdoruesve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/manager.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Init combo box
            Point cBoxLocation = new Point(50, 100);
            Size cBoxSize = new Size(200, 50);
            this.CBox = new DynamicComboBox(
                cBoxSize,
                cBoxLocation
            );
            this.Panel.Controls.Add(CBox.comboBox);
        }

        /**
         * Method to initialize components and fetch necessary data
         */
        
        public void readInitialData() {
            this.controller.init();
        }
    }
}

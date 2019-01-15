using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Manager {
    class Users {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }

        private NavigationBar header;

        public Users(Panel previousPanel) {
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
        }
    }
}

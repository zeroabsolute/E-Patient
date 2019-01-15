using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Models;

namespace Detyra___EPacient.Views {
    class NurseMainPanel
    {
        public User LoggedInUser { get; set; }

        public Panel Panel { get; set; }

        /* Constructor */

        public NurseMainPanel() {
            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "nurseMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Color.ForestGreen;
            this.Panel.Visible = false;
        }
    }
}

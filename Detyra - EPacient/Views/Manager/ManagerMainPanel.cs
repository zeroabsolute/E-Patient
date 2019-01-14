using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;

namespace Detyra___EPacient.Views {
    class ManagerMainPanel
    {
        public User LoggedInUser { get; set; }

        public Panel Panel { get; set; }

        /* Constructor */

        public ManagerMainPanel() {
            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "managerMainPanel";
            this.Panel.Size = new Size(Dimensions.VIEW_WIDTH, Dimensions.VIEW_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Color.MediumPurple;
            this.Panel.Visible = false;
        }
    }
}

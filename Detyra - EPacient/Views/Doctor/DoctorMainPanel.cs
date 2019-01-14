using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Models;

namespace Detyra___EPacient.Views {
    class DoctorMainPanel
    {
        public User LoggedInUser { get; set; }

        public Panel Panel { get; set; }

        /* Constructor */

        public DoctorMainPanel() {
            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "doctorMainPanel";
            this.Panel.Size = new Size(Dimensions.VIEW_WIDTH, Dimensions.VIEW_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Color.IndianRed;
            this.Panel.Visible = false;
        }
    }
}

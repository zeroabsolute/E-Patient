using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Doctor;
using Detyra___EPacient.Views.Manager;
using Detyra___EPacient.Views.Nurse;
using Detyra___EPacient.Views.Operator;

namespace Detyra___EPacient.Views.Common {
    class NavigationBar {
        public ManagerMainPanel ManagerMainPanel { get; set; }
        public OperatorMainPanel OperatorMainPanel { get; set; }
        public DoctorMainPanel DoctorMainPanel { get; set; }
        public NurseMainPanel NurseMainPanel { get; set; }

        private Color backgroundColor;
        private string title;
        private Panel currentPanel;
        private Panel previousPanel;
        private string iconURL;

        public Panel Panel { get; set; }
        private Label titleLabel;
        private Button backButton;
        private PictureBox avatar;

        public NavigationBar(
            Color backgroundColor, 
            string title, 
            Panel currentPanel, 
            Panel previousPanel, 
            string iconURL
        ) {
            this.backgroundColor = backgroundColor;
            this.title = title;
            this.currentPanel = currentPanel;
            this.previousPanel = previousPanel;
            this.iconURL = iconURL;

            int iconDimension = 40;
            Size iconSize = new Size(iconDimension, iconDimension);
            int navBarPaddingVertical = 10;
            int navBarPaddingHorizontal = 20;

            // Init header container
            this.Panel = new Panel();
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "navigationBar";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.NAV_BAR_HEIGHT);
            this.Panel.BackColor = this.backgroundColor;

            // Init back button
            this.backButton = new Button();
            this.backButton.Name = "backButton";
            this.backButton.Location = new Point(navBarPaddingHorizontal, navBarPaddingVertical);
            this.backButton.Size = iconSize;
            this.backButton.Text = "";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.BackColor = Color.Transparent;
            this.backButton.FlatStyle = FlatStyle.Flat;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.backButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.backButton.Image = Image.FromFile("../../Resources/back.png");
            this.backButton.ImageAlign = ContentAlignment.MiddleCenter;
            this.backButton.Click += new EventHandler(onBackButtonClicked);

            this.Panel.Controls.Add(this.backButton);

            // Init title label
            this.titleLabel = new Label();
            this.titleLabel.Location = new Point((navBarPaddingHorizontal + iconSize.Width), navBarPaddingVertical);
            this.titleLabel.Width = Dimensions.PANEL_WIDTH - (2 * navBarPaddingHorizontal + 2 * iconDimension);
            this.titleLabel.Height = Dimensions.NAV_BAR_HEIGHT - 2 * navBarPaddingVertical;
            this.titleLabel.Text = this.title;
            this.titleLabel.Font = new Font(Fonts.primary, 22, FontStyle.Bold);
            this.titleLabel.ForeColor = Colors.WHITE;
            this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.Panel.Controls.Add(titleLabel);

            // Init picture box
            this.avatar = new PictureBox();
            this.avatar.Location = new Point((Dimensions.PANEL_WIDTH - iconDimension - navBarPaddingHorizontal), navBarPaddingVertical);
            this.avatar.Name = "avatar";
            this.avatar.Size = iconSize;
            this.avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.avatar.ImageLocation = this.iconURL;

            this.Panel.Controls.Add(this.avatar);
        }

        /* Event handlers */

        private void onBackButtonClicked(object sender, EventArgs e) {
            Panels.switchPanels(this.currentPanel, this.previousPanel);
        }
    }
}

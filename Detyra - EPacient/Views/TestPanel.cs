using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Views {
    class TestPanel {
        private Panel previousPanel;

        private Panel panel;
        private Button logInBtn = null;

        private int logInBtnHeight = 50;
        private int logInBtnWidth = 200;

        /* Constructor */

        public TestPanel() {
            // Init panel
            this.panel = new Panel();
            this.panel.AutoSize = true;
            this.panel.BackColor = SystemColors.ActiveCaption;
            this.panel.Location = new Point(0, 0);
            this.panel.Name = "logInPanel";
            this.panel.Size = new Size(Dimensions.VIEW_WIDTH, Dimensions.VIEW_HEIGHT);
            this.panel.TabIndex = 0;
            this.panel.BackColor = Color.CornflowerBlue;

            // Init log in button
            int logInBtnX = Panels.getComponentStartingPositionX(Dimensions.VIEW_WIDTH, logInBtnWidth);
            this.logInBtn = new Button();
            this.logInBtn.Location = new Point(logInBtnX, 450);
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new Size(logInBtnWidth, logInBtnHeight);
            this.logInBtn.TabIndex = 1;
            this.logInBtn.Text = "Mbrapa";
            this.logInBtn.UseVisualStyleBackColor = true;
            this.logInBtn.Click += new EventHandler(onBtnClicked);
            this.panel.Controls.Add(this.logInBtn);
        }

        /* Setters */

        public void setPreviousPanel(Panel previousPanel) {
            if (previousPanel != null) {
                this.previousPanel = previousPanel;
            }
        }

        /* Getters */

        public Panel getPanel() {
            return this.panel;
        }

        /* Button click handlers */

        private void onBtnClicked(object sender, EventArgs e) {
            Panels.switchPanels(this.panel, previousPanel);
        }
    }
}

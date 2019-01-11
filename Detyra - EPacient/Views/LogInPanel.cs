using System;
using System.Drawing;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Views {
    class LogInPanel {
        private Panel nextPanelToShow = null;
        private Panel panel = null;
        private Button logInBtn = null;

        private int logInBtnHeight = 50;
        private int logInBtnWidth = 200;

        /* Constructor */

        public LogInPanel() {
            // Init panel
            this.panel = new Panel();
            this.panel.AutoSize = true;
            this.panel.BackColor = SystemColors.ActiveCaption;
            this.panel.Location = new Point(0, 0);
            this.panel.Name = "logInPanel";
            this.panel.Size = new Size(Dimensions.VIEW_WIDTH, Dimensions.VIEW_HEIGHT);
            this.panel.TabIndex = 0;
            this.panel.BackColor = Color.Purple;

            // Init log in button
            int logInBtnX = Panels.getComponentStartingPosition(Dimensions.VIEW_WIDTH, logInBtnWidth);
            this.logInBtn = new Button();
            this.logInBtn.Location = new Point(logInBtnX, 450);
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new Size(logInBtnWidth, logInBtnHeight);
            this.logInBtn.TabIndex = 1;
            this.logInBtn.Text = "Identifikohu";
            this.logInBtn.UseVisualStyleBackColor = true;
            this.logInBtn.Click += new EventHandler(onLogInBtnClicked);
            this.panel.Controls.Add(this.logInBtn);
        }

        /* Setters */

        public void setNextPanel(Panel nextPanel) {
            if (nextPanel != null) {
                this.nextPanelToShow = nextPanel;
            }
        }

        /* Getters */

        public Panel getPanel() {
            return this.panel;
        }

        /* Button click handlers */

        private void onLogInBtnClicked(object sender, EventArgs e) {
            Panels.switchPanels(this.panel, this.nextPanelToShow);
        }
    } 
}

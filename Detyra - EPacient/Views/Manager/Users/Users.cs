using System;
using System.Collections.Generic;
using System.Data;
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
        public DynamicComboBox CBox { get; set; }
        public DynamicTable UsersTable { get; set; }

        public string SelectedRole { get; set; }
        public List<Models.Operator> Operators { get; set; }
        public List<Models.Doctor> Doctors { get; set; }
        public List<Models.Nurse> Nurses { get; set; }

        private NavigationBar header;
        private UsersController controller;
        private GroupBox left;
        private GroupBox right;
        private Label selectRoleLabel;

        private int cardHeight = Dimensions.PANEL_HEIGHT - 100;
        private int bigCardWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
        private int formComponentWidthForKeys;
        private int formComponentWidthForValues;
        private int formComponentHeight;
        private int keyValueMargin = 50;
        private Point tableLocation;
        private Size tableSize;

        public Users(Panel previousPanel) {
            controller = new UsersController(this);

            formComponentWidthForKeys = bigCardWidth / 2 - this.keyValueMargin;
            formComponentWidthForValues = bigCardWidth / 2;
            formComponentHeight = 40;

            this.tableLocation = new Point(Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 100);
            this.tableSize = new Size(
                this.bigCardWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.cardHeight - 110
            );

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

            // Init left container
            left = new GroupBox();
            left.Text = "Lista e përdoruesve të regjistruar";
            left.Location = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, Dimensions.NAV_BAR_HEIGHT + Dimensions.PANEL_PADDING_HORIZONTAL);
            left.Size = new Size(this.bigCardWidth, this.cardHeight);
            left.FlatStyle = FlatStyle.Flat;
            left.Font = new Font(Fonts.primary, 12, FontStyle.Regular);

            this.Panel.Controls.Add(left);

            // Init role label
            this.selectRoleLabel = new Label();
            this.selectRoleLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL, 
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.selectRoleLabel.Width = this.formComponentWidthForKeys;
            this.selectRoleLabel.Height = this.formComponentHeight;
            this.selectRoleLabel.Text = "Roli";
            this.selectRoleLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.selectRoleLabel.ForeColor = Colors.BLACK;

            this.left.Controls.Add(this.selectRoleLabel);

            // Init combo box
            Point cBoxLocation = new Point(
                this.formComponentWidthForKeys + (this.keyValueMargin - Dimensions.PANEL_CARD_PADDING_HORIZONTAL), 
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            Size cBoxSize = new Size(this.formComponentWidthForValues, this.formComponentHeight);
            this.CBox = new DynamicComboBox(
                cBoxSize,
                cBoxLocation
            );
            this.CBox.comboBox.SelectedIndexChanged += new EventHandler(this.onRoleChanged);

            this.left.Controls.Add(CBox.comboBox);

            // Init datagrid view for showing users
            this.UsersTable = new DynamicTable(
                this.tableSize, 
                this.tableLocation,
                this.Operators,
                this.Doctors,
                this.Nurses
            );

            this.left.Controls.Add(this.UsersTable.DataGrid);
        }

        /**
         * Method to initialize components and fetch necessary data
         */
        
        public void readInitialData() {
            this.controller.init();
        }

        /**
         * Event handlers
         */

        private void onRoleChanged(object sender, EventArgs eventArgs) {
            this.controller.handleRoleSelection(sender);
        }
    }
}

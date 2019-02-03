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
    class Services {
        public long SelectedServiceId { get; set; }
        public string SelectedService { get; set; }
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Service> ServicesList { get; set; }
        public DynamicServicesTable ServicesTable { get; set; }
        public Label ServiceLabelValue { get; set; }
        public TextBox NameTxtBox { get; set; }
        public TextBox FeeTxtBox { get; set; }
        public TextBox DescriptionTxtBox { get; set; }

        private ServicesController controller;
        private NavigationBar header;
        private Point tableLocation;
        private Size tableSize;
        private GroupBox right;
        private Label serviceLabel;
        private Label nameLabel;
        private Label feeLabel;
        private Label descriptionLabel;
        private Button submitBtn;
        private Button resetBtn;

        private int tableWidth;
        private int tableHeight;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public Services(Panel previousPanel) {
            // Dimensions
            tableWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
            tableHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
            formComponentKeyWidth = (int) (0.4 * this.rightPanelWidth);
            formComponentValueWidth = (int) (0.5 * this.rightPanelWidth);
            formComponentHorizontalMargin = (int) (0.1 * this.rightPanelWidth - 2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);

            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init controller
            this.controller = new ServicesController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "servicesMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.BAHAMA_BLUE,
                "Menaxhimi i shërbimeve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/manager.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Services table
            this.tableLocation = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, 80);
            this.tableSize = new Size(
                this.tableWidth,
                this.tableHeight
            );

            this.ServicesTable = new DynamicServicesTable(
                this.tableSize,
                this.tableLocation,
                this.ServicesList,
                this.controller
            );
            this.Panel.Controls.Add(this.ServicesTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Shtimi dhe përditësimi i shërbimeve";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.rightPanelWidth),
                80
            );
            right.Size = new Size(this.rightPanelWidth, this.tableHeight);
            right.FlatStyle = FlatStyle.Flat;
            right.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Panel.Controls.Add(this.right);

            // Selected service label
            this.serviceLabel = new Label();
            this.serviceLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.serviceLabel.Width = this.formComponentKeyWidth;
            this.serviceLabel.Height = this.formComponentHeight;
            this.serviceLabel.Text = "Shërbimi";
            this.serviceLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.serviceLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.serviceLabel);

            this.ServiceLabelValue = new Label();
            this.ServiceLabelValue.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.ServiceLabelValue.Width = this.formComponentValueWidth;
            this.ServiceLabelValue.Height = this.formComponentHeight;
            this.ServiceLabelValue.Text = this.SelectedService != null ? this.SelectedService : "-";
            this.ServiceLabelValue.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.ServiceLabelValue.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.ServiceLabelValue);

            /* Init form components */

            // Service name
            this.nameLabel = new Label();
            this.nameLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.nameLabel.Width = this.formComponentKeyWidth;
            this.nameLabel.Height = this.formComponentHeight;
            this.nameLabel.Text = "Emri";
            this.nameLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.nameLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.nameLabel);

            this.NameTxtBox = new TextBox();
            this.NameTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.NameTxtBox.Width = this.formComponentValueWidth;
            this.NameTxtBox.Height = this.formComponentHeight;
            this.NameTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.NameTxtBox);

            // Service fee
            this.feeLabel = new Label();
            this.feeLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.feeLabel.Width = this.formComponentKeyWidth;
            this.feeLabel.Height = this.formComponentHeight;
            this.feeLabel.Text = "Tarifa";
            this.feeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.feeLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.feeLabel);

            this.FeeTxtBox = new TextBox();
            this.FeeTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (3 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.FeeTxtBox.Width = this.formComponentValueWidth;
            this.FeeTxtBox.Height = this.formComponentHeight;
            this.FeeTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.right.Controls.Add(this.FeeTxtBox);

            // Service description
            this.descriptionLabel = new Label();
            this.descriptionLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.descriptionLabel.Width = this.formComponentKeyWidth;
            this.descriptionLabel.Height = this.formComponentHeight;
            this.descriptionLabel.Text = "Përshkrimi";
            this.descriptionLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.descriptionLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.descriptionLabel);

            this.DescriptionTxtBox = new TextBox();
            this.DescriptionTxtBox.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                (4 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.DescriptionTxtBox.Width = this.formComponentValueWidth;
            this.DescriptionTxtBox.Height = (int) (3 * this.formComponentHeight);
            this.DescriptionTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.DescriptionTxtBox.Multiline = true;
            this.right.Controls.Add(this.DescriptionTxtBox);

            /* Buttons */

            this.resetBtn = new Button();
            this.resetBtn.Size = new Size(this.formComponentKeyWidth, this.formComponentHeight);
            this.resetBtn.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                this.tableHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.resetBtn.Text = "RESET";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.resetBtn.ForeColor = Colors.WHITE;
            this.resetBtn.BackColor = Colors.IMPERIAL_RED;
            this.resetBtn.FlatStyle = FlatStyle.Flat;
            this.resetBtn.Click += new EventHandler(onResetButtonClicked);
            this.resetBtn.Image = Image.FromFile("../../Resources/clear.png");
            this.resetBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.resetBtn);

            this.submitBtn = new Button();
            this.submitBtn.Size = new Size(this.formComponentKeyWidth, this.formComponentHeight);
            this.submitBtn.Location = new Point(
                this.rightPanelWidth - (this.formComponentKeyWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.tableHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.submitBtn.Text = "RUAJ";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.submitBtn.ForeColor = Colors.WHITE;
            this.submitBtn.BackColor = Colors.MALACHITE;
            this.submitBtn.FlatStyle = FlatStyle.Flat;
            this.submitBtn.Click += new EventHandler(onSubmitButtonClicked);
            this.submitBtn.Image = Image.FromFile("../../Resources/save.png");
            this.submitBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.submitBtn);
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

        private void onResetButtonClicked(object sender, EventArgs eventArgs) {
            controller.handleResetButton();
        }

        private void onSubmitButtonClicked(object sender, EventArgs eventArgs) {
            controller.handleSubmitButton();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Operator;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Operator {
    class PatientCharts {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Patient> PatientsList { get; set; }
        public PatientsTable PatientsTable { get; set; }
        public Label PatientLabelValue { get; set; }
        public Models.Patient SelectedPatient { get; set; }

        private PatientChartsController controller;
        private NavigationBar header;
        private Label patientLabel;
        private Point tableLocation;
        private Size tableSize;
        private GroupBox right;

        private int tableWidth;
        private int tableHeight;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public PatientCharts(Panel previousPanel) {
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
            this.controller = new PatientChartsController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "patientChartsMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.PERSIAN_INDIGO,
                "Kartelat e pacientëve",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/operator.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Patients table
            this.tableLocation = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, 80);
            this.tableSize = new Size(
                this.tableWidth,
                this.tableHeight
            );

            this.PatientsTable = new PatientsTable(
                this.tableSize,
                this.tableLocation,
                this.PatientsList,
                this.controller
            );
            this.Panel.Controls.Add(this.PatientsTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Kartela mjekësore";
            right.Location = new Point(
                Dimensions.PANEL_WIDTH - (Dimensions.PANEL_PADDING_HORIZONTAL + this.rightPanelWidth),
                80
            );
            right.Size = new Size(this.rightPanelWidth, this.tableHeight);
            right.FlatStyle = FlatStyle.Flat;
            right.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Panel.Controls.Add(this.right);

            // Selected patient label
            this.patientLabel = new Label();
            this.patientLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.patientLabel.Width = this.formComponentKeyWidth;
            this.patientLabel.Height = this.formComponentHeight;
            this.patientLabel.Text = "Pacienti";
            this.patientLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.patientLabel.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.patientLabel);

            this.PatientLabelValue = new Label();
            this.PatientLabelValue.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL + this.formComponentKeyWidth + this.formComponentHorizontalMargin,
                Dimensions.PANEL_CARD_PADDING_VERTICAL * 2
            );
            this.PatientLabelValue.Width = this.formComponentValueWidth;
            this.PatientLabelValue.Height = this.formComponentHeight;
            this.PatientLabelValue.Text = "-";
            this.PatientLabelValue.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.PatientLabelValue.ForeColor = Colors.BLACK;
            this.right.Controls.Add(this.PatientLabelValue);
        }

        /**
         * Method to initialize components and fetch necessary data
         */

        public void readInitialData() {
            this.controller.init();
        }
    }
}

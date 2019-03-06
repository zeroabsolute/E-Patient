using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Nurse;
using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;

namespace Detyra___EPacient.Views.Nurse {
    public class AnalysisNurse {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Patient> PatientsList { get; set; }
        public List<Models.ChartDocument> AnalysisList { get; set; }
        public PatientsTable PatientsTable { get; set; }
        public AnalysisTable AnalysisTable { get; set; }
        public Label PatientLabelValue { get; set; }
        public Models.Patient SelectedPatient { get; set; }

        private AnalysisController controller;
        private NavigationBar header;
        private Label patientLabel;
        private Label analysisLabel;
        private Button addAnalysisBtn;
        private GroupBox right;

        private int tableWidth;
        private int tableHeight;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public AnalysisNurse(Panel previousPanel) {
            // Dimensions
            tableWidth = (int)(Dimensions.PANEL_WIDTH * 0.4);
            tableHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int)(Dimensions.PANEL_WIDTH * 0.5);
            formComponentKeyWidth = (int)(0.4 * this.rightPanelWidth);
            formComponentValueWidth = (int)(0.5 * this.rightPanelWidth);
            formComponentHorizontalMargin = (int)(0.1 * this.rightPanelWidth - 2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);
            // Init previous panel
            this.PreviousPanel = previousPanel;

            // Init controller
            this.controller = new AnalysisController(this);

            // Init panel
            this.Panel = new Panel();
            this.Panel.AutoSize = true;
            this.Panel.Location = new Point(0, 0);
            this.Panel.Name = "analysisMainPanel";
            this.Panel.Size = new Size(Dimensions.PANEL_WIDTH, Dimensions.PANEL_HEIGHT);
            this.Panel.TabIndex = 0;
            this.Panel.BackColor = Colors.WHITE;
            this.Panel.Visible = false;

            // Init header
            this.header = new NavigationBar(
                Colors.IMPERIAL_RED,
                "Analizat",
                this.Panel,
                this.PreviousPanel,
                "../../Resources/nurse.png"
            );
            this.Panel.Controls.Add(this.header.Panel);

            // Patients table
            Point patientsTableLocation = new Point(Dimensions.PANEL_PADDING_HORIZONTAL, 80);
            Size patientsTableSize = new Size(
                this.tableWidth,
                this.tableHeight
            );

            this.PatientsTable = new PatientsTable(
                patientsTableSize,
                patientsTableLocation,
                this.PatientsList,
                this.controller
            );
            this.Panel.Controls.Add(this.PatientsTable.DataGrid);

            // Init right container
            right = new GroupBox();
            right.Text = "Analiza mjekësore";
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

            // Docs section header
            this.analysisLabel = new Label();
            this.analysisLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.analysisLabel.Width = this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);
            this.analysisLabel.Height = this.formComponentHeight;
            this.analysisLabel.Text = "Analizat";
            this.analysisLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.analysisLabel.ForeColor = Colors.BLACK;
            this.analysisLabel.BackColor = Colors.WHITE_LILAC;
            this.analysisLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.analysisLabel);

            this.addAnalysisBtn = new Button();
            this.addAnalysisBtn.Size = new Size(this.formComponentHeight, this.formComponentHeight);
            this.addAnalysisBtn.Location = new Point(
                this.rightPanelWidth - Dimensions.PANEL_CARD_PADDING_HORIZONTAL - this.formComponentHeight,
                (this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.addAnalysisBtn.Text = "";
            this.addAnalysisBtn.UseVisualStyleBackColor = true;
            this.addAnalysisBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.addAnalysisBtn.ForeColor = Colors.WHITE;
            this.addAnalysisBtn.BackColor = Colors.WHITE_LILAC;
            this.addAnalysisBtn.ImageAlign = ContentAlignment.MiddleCenter;
            this.addAnalysisBtn.FlatAppearance.BorderColor = Colors.WHITE_LILAC;
            this.addAnalysisBtn.FlatAppearance.CheckedBackColor = Colors.WHITE_LILAC;
            this.addAnalysisBtn.FlatAppearance.MouseDownBackColor = Colors.WHITE_LILAC;
            this.addAnalysisBtn.FlatAppearance.MouseOverBackColor = Colors.WHITE_LILAC;
            this.addAnalysisBtn.Image = Image.FromFile("../../Resources/add.png");
            this.addAnalysisBtn.FlatStyle = FlatStyle.Flat;
            this.addAnalysisBtn.Click += new EventHandler(onAddAnalysisClicked);
            this.right.Controls.Add(this.addAnalysisBtn);
            this.right.Controls.SetChildIndex(this.addAnalysisBtn, 0);

            ToolTip addDocTooltip = new ToolTip();
            addDocTooltip.SetToolTip(this.addAnalysisBtn, "Shto analizë");

            // Docs table
            Point analysisTableLocation = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            Size analysisTableSize = new Size(
                this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.tableHeight - 150
            );

            this.AnalysisTable = new AnalysisTable(
                analysisTableSize,
                analysisTableLocation,
                this.AnalysisList,
                this.controller
            );
            this.right.Controls.Add(this.AnalysisTable.DataGrid);

        }

        public void readInitialData() {
            this.controller.init();
        }

        /**
         * Event handlers 
         */

        private void onAddAnalysisClicked(object sender, EventArgs eventArgs) {
            this.controller.handleAddAnalysis();
        }
    }
}
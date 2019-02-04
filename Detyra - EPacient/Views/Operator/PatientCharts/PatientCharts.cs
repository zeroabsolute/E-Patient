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
    public class OperatorPatientCharts {
        public Panel PreviousPanel { get; set; }
        public Panel Panel { get; set; }
        public List<Models.Patient> PatientsList { get; set; }
        public List<Models.ChartDocument> DocsList { get; set; }
        public List<Models.Allergen> AllergensList { get; set; }
        public PatientsTable PatientsTable { get; set; }
        public DocsTable DocsTable { get; set; }
        public AllergensTable AllergensTable { get; set; }
        public Label PatientLabelValue { get; set; }
        public Models.Patient SelectedPatient { get; set; }

        private PatientChartsController controller;
        private NavigationBar header;
        private Label patientLabel;
        private Label docsLabel;
        private Button addDocsBtn;
        private Label allergensLabel;
        private Button addAllergenBtn;
        private Button printPatientChartBtn;
        private GroupBox right;

        private int tableWidth;
        private int tableHeight;
        private int rightPanelWidth;
        private int formComponentVerticalMargin = 50;
        private int formComponentKeyWidth;
        private int formComponentValueWidth;
        private int formComponentHeight = 40;
        private int formComponentHorizontalMargin;

        public OperatorPatientCharts(Panel previousPanel) {
            // Dimensions
            tableWidth = (int) (Dimensions.PANEL_WIDTH * 0.4);
            tableHeight = Dimensions.PANEL_HEIGHT - (Dimensions.NAV_BAR_HEIGHT + 40);
            rightPanelWidth = (int) (Dimensions.PANEL_WIDTH * 0.5);
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

            // Docs section header
            this.docsLabel = new Label();
            this.docsLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.docsLabel.Width = this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);
            this.docsLabel.Height = this.formComponentHeight;
            this.docsLabel.Text = "Dokumentat e kartelës";
            this.docsLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.docsLabel.ForeColor = Colors.BLACK;
            this.docsLabel.BackColor = Colors.WHITE_LILAC;
            this.docsLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.docsLabel);

            this.addDocsBtn = new Button();
            this.addDocsBtn.Size = new Size(this.formComponentHeight, this.formComponentHeight);
            this.addDocsBtn.Location = new Point(
                this.rightPanelWidth - Dimensions.PANEL_CARD_PADDING_HORIZONTAL - this.formComponentHeight,
                (this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.addDocsBtn.Text = "";
            this.addDocsBtn.UseVisualStyleBackColor = true;
            this.addDocsBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.addDocsBtn.ForeColor = Colors.WHITE;
            this.addDocsBtn.BackColor = Colors.WHITE_LILAC;
            this.addDocsBtn.ImageAlign = ContentAlignment.MiddleCenter;
            this.addDocsBtn.FlatAppearance.BorderColor = Colors.WHITE_LILAC;
            this.addDocsBtn.FlatAppearance.CheckedBackColor = Colors.WHITE_LILAC;
            this.addDocsBtn.FlatAppearance.MouseDownBackColor = Colors.WHITE_LILAC;
            this.addDocsBtn.FlatAppearance.MouseOverBackColor = Colors.WHITE_LILAC;
            this.addDocsBtn.Image = Image.FromFile("../../Resources/add.png");
            this.addDocsBtn.FlatStyle = FlatStyle.Flat;
            this.addDocsBtn.Click += new EventHandler(onAddDocsClicked);
            this.right.Controls.Add(this.addDocsBtn);
            this.right.Controls.SetChildIndex(this.addDocsBtn, 0);

            ToolTip addDocTooltip = new ToolTip();
            addDocTooltip.SetToolTip(this.addDocsBtn, "Shto dokument");

            // Docs table
            Point docsTableLocation = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (2 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            Size docsTableSize = new Size(
                this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                180
            );

            this.DocsTable = new DocsTable(
                docsTableSize,
                docsTableLocation,
                this.DocsList,
                this.controller
            );
            this.right.Controls.Add(this.DocsTable.DataGrid);

            // Allergens section header
            this.allergensLabel = new Label();
            this.allergensLabel.Location = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.allergensLabel.Width = this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL);
            this.allergensLabel.Height = this.formComponentHeight;
            this.allergensLabel.Text = "Alergenët";
            this.allergensLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.allergensLabel.ForeColor = Colors.BLACK;
            this.allergensLabel.BackColor = Colors.WHITE_LILAC;
            this.allergensLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.right.Controls.Add(this.allergensLabel);

            this.addAllergenBtn = new Button();
            this.addAllergenBtn.Size = new Size(this.formComponentHeight, this.formComponentHeight);
            this.addAllergenBtn.Location = new Point(
                this.rightPanelWidth - Dimensions.PANEL_CARD_PADDING_HORIZONTAL - this.formComponentHeight,
                (6 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            this.addAllergenBtn.Text = "";
            this.addAllergenBtn.UseVisualStyleBackColor = true;
            this.addAllergenBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.addAllergenBtn.ForeColor = Colors.WHITE;
            this.addAllergenBtn.BackColor = Colors.WHITE_LILAC;
            this.addAllergenBtn.ImageAlign = ContentAlignment.MiddleCenter;
            this.addAllergenBtn.FlatAppearance.BorderColor = Colors.WHITE_LILAC;
            this.addAllergenBtn.FlatAppearance.CheckedBackColor = Colors.WHITE_LILAC;
            this.addAllergenBtn.FlatAppearance.MouseDownBackColor = Colors.WHITE_LILAC;
            this.addAllergenBtn.FlatAppearance.MouseOverBackColor = Colors.WHITE_LILAC;
            this.addAllergenBtn.Image = Image.FromFile("../../Resources/add.png");
            this.addAllergenBtn.FlatStyle = FlatStyle.Flat;
            this.addAllergenBtn.Click += new EventHandler(onAddAllergensClicked);
            this.right.Controls.Add(this.addAllergenBtn);
            this.right.Controls.SetChildIndex(this.addAllergenBtn, 0);

            ToolTip addAllergenTooltip = new ToolTip();
            addAllergenTooltip.SetToolTip(this.addAllergenBtn, "Shto alergen");

            // Allergens table
            Point allergensTableLocation = new Point(
                Dimensions.PANEL_CARD_PADDING_HORIZONTAL,
                (7 * this.formComponentVerticalMargin) + (Dimensions.PANEL_CARD_PADDING_VERTICAL * 2)
            );
            Size allergensTableSize = new Size(
                this.rightPanelWidth - (2 * Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                130
            );

            this.AllergensTable = new AllergensTable(
                allergensTableSize,
                allergensTableLocation,
                this.AllergensList,
                this.controller
            );
            this.right.Controls.Add(this.AllergensTable.DataGrid);

            // Print button

            this.printPatientChartBtn = new Button();
            this.printPatientChartBtn.Size = new Size(this.formComponentValueWidth, this.formComponentHeight);
            this.printPatientChartBtn.Location = new Point(
                this.rightPanelWidth - (this.formComponentValueWidth + Dimensions.PANEL_CARD_PADDING_HORIZONTAL),
                this.tableHeight - (Dimensions.PANEL_CARD_PADDING_VERTICAL + this.formComponentHeight)
            );
            this.printPatientChartBtn.Text = "PRINTO KARTELËN";
            this.printPatientChartBtn.UseVisualStyleBackColor = true;
            this.printPatientChartBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.printPatientChartBtn.Image = Image.FromFile("../../Resources/print.png");
            this.printPatientChartBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.printPatientChartBtn.ForeColor = Colors.WHITE;
            this.printPatientChartBtn.BackColor = Colors.PERSIAN_INDIGO;
            this.printPatientChartBtn.FlatStyle = FlatStyle.Flat;
            this.printPatientChartBtn.Click += new EventHandler(onPrintChartClicked);
            this.right.Controls.Add(this.printPatientChartBtn);
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

        private void onAddDocsClicked(object sender, EventArgs eventArgs) {
            this.controller.handleAddDoc();
        }

        private void onAddAllergensClicked(object sender, EventArgs eventArgs) {
            this.controller.handleAddAllergen();
        }

        private void onPrintChartClicked(object sender, EventArgs eventArgs) {
            this.controller.handlePrintChart();
        }
    }
}

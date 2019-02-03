using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Controllers.Operator;
using Detyra___EPacient.Styles;

namespace Detyra___EPacient.Views.Operator {
    public class DocsTable {
        public DataTable Table { get; set; }
        public DataGridView DataGrid { get; set; }

        private Size tableSize;
        private Point tableLocation;
        private List<Models.ChartDocument> docs;
        private PatientChartsController controller;

        public DocsTable(
            Size tableSize, 
            Point tableLocation, 
            List<Models.ChartDocument> docs,
            PatientChartsController controller
        ) {
            // Init size; location; data source
            this.tableLocation = tableLocation;
            this.tableSize = tableSize;
            this.docs = docs;
            this.controller = controller;

            // Init table
            Table = new DataTable();

            // Init datagrid
            DataGrid = new DataGridView();
            DataGrid.ReadOnly = true;
            DataGrid.AllowUserToAddRows = false;
            DataGrid.Size = this.tableSize;
            DataGrid.Location = this.tableLocation;
            DataGrid.RowTemplate.Height = 40;
            DataGrid.ColumnHeadersHeight = 40;
            DataGrid.BackgroundColor = Colors.ALTO;
            DataGrid.ColumnCount = 5;
            DataGrid.Columns[0].Name = "ID";
            DataGrid.Columns[1].Name = "Emri";
            DataGrid.Columns[2].Name = "Tipi";
            DataGrid.Columns[3].Name = "URL";
            DataGrid.Columns[4].Name = "Data e krijimit";
            DataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            DataGrid.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            DataGrid.MultiSelect = false;
            DataGrid.SelectionChanged += new EventHandler(onSelectionChanged);
        }

        /*
         * Event handlers
         */

        private void onSelectionChanged(object sender, EventArgs e) {
            
        }
    }
}

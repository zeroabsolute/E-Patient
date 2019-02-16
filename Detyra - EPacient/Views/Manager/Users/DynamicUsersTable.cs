using Detyra___EPacient.Controllers.Manager;
using Detyra___EPacient.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Detyra___EPacient.Views.Common {
    class DynamicTable {
        public DataTable Table { get; set; }
        public DataGridView DataGrid { get; set; }

        private Size tableSize;
        private Point tableLocation;
        private List<Models.Operator> operators;
        private List<Models.Doctor> doctors;
        private List<Models.Nurse> nurses;
        private UsersController controller;

        public DynamicTable(
            Size tableSize, 
            Point tableLocation, 
            List<Models.Operator> operators,
            List<Models.Doctor> doctors,
            List<Models.Nurse> nurses,
            UsersController controller
        ) {
            // Init size; location; data source
            this.tableLocation = tableLocation;
            this.tableSize = tableSize;
            this.operators = operators;
            this.doctors = doctors;
            this.nurses = nurses;
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
            DataGrid.Columns[1].Name = "Email";
            DataGrid.Columns[2].Name = "Emri";
            DataGrid.Columns[3].Name = "Mbiemri";
            DataGrid.Columns[4].Name = "Status";
            DataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            DataGrid.SelectionChanged += new EventHandler(onSelectionChanged);
        }

        /*
         * Event handlers
         */

        private void onSelectionChanged(object sender, EventArgs e) {
            this.controller.handleTableRowSelection();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Controllers.Nurse;
using Detyra___EPacient.Styles;

namespace Detyra___EPacient.Views.Nurse {
    class NurseReservationsTable {
        public DataTable Table { get; set; }
        public DataGridView DataGrid { get; set; }

        private Size tableSize;
        private Point tableLocation;
        private ReservationsNurseController controller;

        public NurseReservationsTable(
            Size tableSize, 
            Point tableLocation, 
            ReservationsNurseController controller
        ) {
            // Init size; location; data source
            this.tableLocation = tableLocation;
            this.tableSize = tableSize;
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
            DataGrid.ColumnCount = 6;
            DataGrid.Columns[0].Name = "ID";
            DataGrid.Columns[1].Name = "Fillimi";
            DataGrid.Columns[2].Name = "Fundi";
            DataGrid.Columns[3].Name = "Pacienti";
            DataGrid.Columns[4].Name = "Mjeku";
            DataGrid.Columns[5].Name = "Shërbimi";
            DataGrid.Columns[0].Width = 40;
            DataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            DataGrid.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            DataGrid.MultiSelect = false;
        }
    }
}

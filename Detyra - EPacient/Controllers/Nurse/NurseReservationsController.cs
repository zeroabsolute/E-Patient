using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Nurse;

namespace Detyra___EPacient.Controllers.Nurse {
    class ReservationsNurseController {
        private ReservationsNurse view;

        private Models.Employee employeeModel;
        private Models.Nurse nurseModel;
        private Models.Reservation reservationModel;
        private List<Models.Reservation> reservations;

        public ReservationsNurseController(ReservationsNurse view) {
            this.view = view;
            this.reservationModel = new Models.Reservation();
            this.employeeModel = new Models.Employee();
            this.nurseModel = new Models.Nurse();
        }

        /**
         * Helper to populate table with data
         */

        private void populateTable(List<Models.Reservation> data) {
            this.view.ReservationsTableNurse.DataGrid.Rows.Clear();
            this.view.ReservationsTableNurse.DataGrid.Refresh();

            data.ForEach((item) => {
                this.view.ReservationsTableNurse.DataGrid.Rows.Add(
                    item.Id,
                    item.StartDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
                    item.EndDateTime.ToString(DateTimeFormats.SQ_DATE_TIME),
                    $"{item.Patient.FirstName} {item.Patient.LastName}",
                    $"{item.Doctor.Employee.FirstName} {item.Doctor.Employee.LastName}",
                    $"{item.Service.Name}"
                );
            });
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read reservations from DB and populate table
                int id = await nurseModel.getNurseByUserId(this.view.LoggedInUser.Id);

                List<Models.Reservation> reservations = await this.reservationModel.readReservationsForNurses(id);
                this.reservations = reservations;
                this.populateTable(reservations);

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

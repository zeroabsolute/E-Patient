using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Helpers;
using Detyra___EPacient.Views.Doctor;

namespace Detyra___EPacient.Controllers.Doctor{
    class DoctorMainController {
        private DoctorMainPanel view;

        public DoctorMainController(DoctorMainPanel view) {
            this.view = view;
        }

        /**
         * Handler for menu button click.
         * Redirects user to one of the operator panels.
         */

        public void handleMenuButtonClick(String eventOrigin) {
            switch (eventOrigin) {
                case DoctorMainPanel.TIME_TABLES_DOC_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.TimeTablesDocPanel.Panel);
                    this.view.TimeTablesDocPanel.LoggedInUser = this.view.LoggedInUser;
                    this.view.TimeTablesDocPanel.readInitialData();
                    break;
                case DoctorMainPanel.RESERVATIONS_DOC_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.ReservationsDocPanel.Panel);
                    this.view.ReservationsDocPanel.LoggedInUser = this.view.LoggedInUser;
                    this.view.ReservationsDocPanel.readInitialData();
                    break;
                case DoctorMainPanel.PRESCRIPTION_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.PrescriptionPanel.Panel);
                    this.view.PrescriptionPanel.LoggedInUser = this.view.LoggedInUser;
                    this.view.PrescriptionPanel.readInitialData();
                    break;
                case DoctorMainPanel.LOG_OUT_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.LogInPanel.Panel);
                    break;
            }
        }
    }
}

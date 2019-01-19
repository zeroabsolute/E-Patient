using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Views;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Controllers.Operator{
    class OperatorMainController {
        private OperatorMainPanel view;

        public OperatorMainController(OperatorMainPanel view) {
            this.view = view;
        }

        /**
         * Handler for menu button click.
         * Redirects user to one of the operator panels.
         */

        public void handleMenuButtonClick(String eventOrigin) {
            switch (eventOrigin) {
                case OperatorMainPanel.PATIENTS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.PatientsPanel.Panel);
                    break;
                case OperatorMainPanel.RESERVATIONS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.ReservationsPanel.Panel);
                    break;
                case OperatorMainPanel.TIME_TABLES_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.TimeTablesPanel.Panel);
                    break;
                case OperatorMainPanel.LOG_OUT_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.LogInPanel.Panel);
                    break;
            }
        }
    }
}

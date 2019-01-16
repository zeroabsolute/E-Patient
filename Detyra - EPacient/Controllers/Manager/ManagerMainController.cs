using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Views;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Controllers.Manager {
    class ManagerMainController {
        private ManagerMainPanel view;

        public ManagerMainController(ManagerMainPanel view) {
            this.view = view;
        }

        /**
         * Handler for menu button click.
         * Redirects user to one of the manager panels.
         */

        public void handleMenuButtonClick(String eventOrigin) {
            switch (eventOrigin) {
                case ManagerMainPanel.USERS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.UsersPanel.Panel);
                    this.view.UsersPanel.readInitialData();
                    break;
                case ManagerMainPanel.SERVICES_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.ServicesPanel.Panel);
                    break;
                case ManagerMainPanel.TIMETABLES_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.TimetablesPanel.Panel);
                    break;
                case ManagerMainPanel.MEDICAMENTS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.MedicamentsPanel.Panel);
                    break;
                case ManagerMainPanel.DOCTOR_IN_CHARGE_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.DoctorInChargePanel.Panel);
                    break;
                case ManagerMainPanel.ANALYTICS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.AnalyticsPanel.Panel);
                    break;
                case ManagerMainPanel.LOG_OUT_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.LogInPanel.Panel);
                    break;
            }
        }
    }
}

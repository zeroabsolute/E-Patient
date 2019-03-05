using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Views.Nurse;
using Detyra___EPacient.Helpers;

namespace Detyra___EPacient.Controllers.Nurse {
    class NurseMainController {
        private NurseMainPanel view;

        public NurseMainController(NurseMainPanel view) {
            this.view = view;
        }

        /**
         * Handler for menu button click.
         * Redirects user to one of the operator panels.
         */

        public void handleMenuButtonClick(String eventOrigin) {
            switch (eventOrigin) {     
                case NurseMainPanel.SCHEDULE_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.SchedulePanel.Panel);
                    this.view.SchedulePanel.LoggedInUser = this.view.LoggedInUser;
                    this.view.SchedulePanel.readInitialData();
                    break;
                case NurseMainPanel.RESERVATIONS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.ReservationsNursePanel.Panel);
                    this.view.ReservationsNursePanel.LoggedInUser = this.view.LoggedInUser;
                    this.view.ReservationsNursePanel.readInitialData();
                    break;
                case NurseMainPanel.ANALYSIS_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.AnalysisPanel.Panel);
                    this.view.AnalysisPanel.readInitialData();
                    break;
                case NurseMainPanel.LOG_OUT_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.LogInPanel.Panel);
                    break;
            }
        }
    }
}

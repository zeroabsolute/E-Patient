using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Views;
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
                    break;
                case NurseMainPanel.LOG_OUT_BTN:
                    Panels.switchPanels(this.view.Panel, this.view.LogInPanel.Panel);
                    break;
            }
        }
    }
}

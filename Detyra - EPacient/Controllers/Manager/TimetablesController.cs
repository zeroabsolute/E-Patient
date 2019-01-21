using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class TimetablesController {
        private Timetables view;
        
        private Models.Employee employeeModel;

        public TimetablesController(Timetables view) {
            this.view = view;
        }
    }
}

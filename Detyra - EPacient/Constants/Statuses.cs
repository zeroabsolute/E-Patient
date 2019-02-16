using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detyra___EPacient.Constants {
    class Statuses {
        public static Models.Status ACTIVE = new Models.Status(1, "Aktiv");
        public static Models.Status INACTIVE = new Models.Status(0, "Inaktiv");
    }
}

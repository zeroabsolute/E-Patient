using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Detyra___EPacient.Helpers {
    class Panels {
        public static void switchPanels(Panel panelToHide, Panel panelToShow) {
            panelToHide.Visible = false;
            panelToShow.Visible = true;
        }

        public static int getComponentStartingPosition(int parentWidth, int componentWidth) {
            return (int) ((parentWidth - componentWidth ) / 2);
        }
    }
}

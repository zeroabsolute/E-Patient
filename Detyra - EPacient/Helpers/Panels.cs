using Detyra___EPacient.Constants;
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

        public static int getComponentStartingPositionX(int parentWidth, int componentWidth) {
            return (int) ((parentWidth - componentWidth ) / 2);
        }

        public static int getComponentStartingPositionY(int parentHeight, int componentHeight) {
            return (int) ((parentHeight - componentHeight - Dimensions.TOOLBAR_HEIGHT) / 2);
        }
    }
}

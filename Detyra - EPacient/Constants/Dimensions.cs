using Detyra___EPacient.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Detyra___EPacient.Constants {
    class Dimensions {

        /**
         * Standard dimensions 
         */

        public static int TOOLBAR_HEIGHT = 40;
        public static int VIEW_HEIGHT = Screen.PrimaryScreen.WorkingArea.Height;
        public static int VIEW_WIDTH = Screen.PrimaryScreen.WorkingArea.Width;
        public static int PANEL_HEIGHT = VIEW_HEIGHT - TOOLBAR_HEIGHT;
        public static int PANEL_WIDTH = VIEW_WIDTH - 16;

        /**
         * Panel menu dimensions and coordinates
         */

        public static int PANEL_PADDING_HORIZONTAL = 20;
        public static int PANEL_PADDING_TOP = 40;
        public static int PANEL_CARD_PADDING_HORIZONTAL = 8;
        public static int PANEL_CARD_PADDING_VERTICAL = 20;

        public static int AVATAR_SIZE = VIEW_HEIGHT - 300;
        public static int AVATAR_Y_COORD = Panels.getComponentStartingPositionY(Dimensions.VIEW_HEIGHT, AVATAR_SIZE);
        public static Point AVATAR_LOCATION = new Point(20, AVATAR_Y_COORD);

        public static int MENU_CONTAINER_WIDTH = (int) Dimensions.VIEW_WIDTH / 2;
        public static int MENU_CONTAINER_HEIGHT = Dimensions.VIEW_HEIGHT - 100;
        public static int MENU_CONTAINER_X_COORD = Dimensions.VIEW_WIDTH - MENU_CONTAINER_WIDTH - 40;
        public static int MENU_CONTAINER_Y_COORD = Panels.getComponentStartingPositionY(Dimensions.VIEW_HEIGHT, MENU_CONTAINER_HEIGHT);
        public static Point MENU_LOCATION = new Point(MENU_CONTAINER_X_COORD, MENU_CONTAINER_Y_COORD);

        /**
         * Navigation bar dimensions
         */

        public static int NAV_BAR_HEIGHT = 60;

        /**
         * Analytics component dimensions
         */

        public static Size TOP_CARD_SIZE = new Size(300, 130);
    }
}

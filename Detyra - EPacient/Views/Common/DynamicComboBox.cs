using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Styles;

namespace Detyra___EPacient.Views.Common {
    class DynamicComboBox {
        public ComboBox comboBox { get; set; }

        private Size comboBoxSize;
        private Point location;

        public DynamicComboBox(Size size, Point location) {
            this.comboBoxSize = size;
            this.location = location;

            // Init combobox
            comboBox = new ComboBox();
            comboBox.Location = this.location;
            comboBox.Name = "dynamicComboBox";
            comboBox.Size = this.comboBoxSize;
            comboBox.BackColor = Colors.WHITE;
            comboBox.ForeColor = Colors.BLACK;
            comboBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
        }
    }
}

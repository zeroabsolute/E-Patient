using Detyra___EPacient.Constants;
using Detyra___EPacient.Controllers.Operator;
using Detyra___EPacient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Controllers.Nurse;

namespace Detyra___EPacient.Views.Nurse.Analysis {

    public partial class AddAnalysisForm : Form {
        private AnalysisController controller;
        private string url;

        public AddAnalysisForm(AnalysisController controller) {
            InitializeComponent();
            this.controller = controller;
        }

        public void onFormLoad(object sender, EventArgs e) {
            this.docTypesComboBox.comboBox.DataSource = new List<string> {
                ChartDocTypes.ANALIZE,
            };
        }

        public void onSubmitButtonClicked(object sender, EventArgs e) {
            if (this.url == "") {
                string message = "Nuk keni zgjedhur dokumentin";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            if (this.nameTxtBox.Text == "") {
                string message = "Nuk keni vendosur emrin e dokumentit";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            this.controller.handleDocSubmit(
                this.url,
                this.nameTxtBox.Text,
                this.docTypesComboBox.comboBox.SelectedValue.ToString()
            );
        }

        public void onChoseFileClicked(object sender, EventArgs e) {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Zgjidhni dokumentin";
            opf.Multiselect = false;

            if (opf.ShowDialog() == DialogResult.OK) {
                this.url = opf.FileName;
            }
        }
    }
}

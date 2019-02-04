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

namespace Detyra___EPacient.Views.Operator.PatientCharts {
    public partial class AddAllergensForm : Form {
        private PatientChartsController controller;

        public AddAllergensForm(PatientChartsController controller) {
            InitializeComponent();
            this.controller = controller;
        }

        public void onFormLoad(object sender, EventArgs e) {
            if (this.controller.medicaments != null && this.controller.medicaments.Count > 0) {
                this.controller.medicaments.ForEach((item) => {
                    this.medicamentsListBox.Items.Add(item);
                });
            }
        }

        public void onSubmitButtonClicked(object sender, EventArgs e) {
            if (this.medicamentsListBox.SelectedItem == null) {
                string message = "Nuk keni zgjedhur medikamentin";
                MessageBox.Show(message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            this.controller.handleAllergenSubmit(((Models.Medicament) this.medicamentsListBox.SelectedItem).Id);
        }
    }
}

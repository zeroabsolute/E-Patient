using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class MedicamentsController {
        private Medicaments view;

        private Models.Medicament medicamentModel;

        public MedicamentsController(Medicaments view) {
            this.view = view;
            this.medicamentModel = new Models.Medicament();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read services from DB and populate table
                List<Models.Medicament> medicaments = await this.medicamentModel.readMedicaments();

                this.view.MedicamentsTable.DataGrid.Rows.Clear();
                this.view.MedicamentsTable.DataGrid.Refresh();

                medicaments.ForEach((item) => {
                    this.view.MedicamentsTable.DataGrid.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Description,
                        item.ExpirationDate.ToString(DateTimeFormats.SQ_DATE),
                        item.Ingredients
                    );
                });

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle selection of a table row
         */

        public void handleTableRowSelection() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                var selectedRow = this.view.MedicamentsTable.DataGrid.SelectedRows.Count > 0
                ? this.view.MedicamentsTable.DataGrid.SelectedRows[0]
                : null;

                if (selectedRow != null) {
                    int id = (int) selectedRow.Cells[0].Value;
                    string name = selectedRow.Cells[1].Value.ToString();
                    string description = selectedRow.Cells[2].Value.ToString();
                    DateTime expirationDate = DateTime.ParseExact(selectedRow.Cells[3].Value.ToString(), "dd-MM-yyyy", null);
                    string ingredients = selectedRow.Cells[4].Value.ToString();

                    this.view.SelectedMedicament = name;
                    this.view.SelectedMedicamentId = id;
                    this.view.MedicamentLabelValue.Text = name;
                    this.view.NameTxtBox.Text = name;
                    this.view.DescriptionTxtBox.Text = description;
                    this.view.ExpirationDatePicker.Value = expirationDate;
                    this.view.IngredientsTxtBox.Text = ingredients;
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Controller to handle reset button
         */
        
        public void handleResetButton() {
            this.view.SelectedMedicament = null;
            this.view.SelectedMedicamentId = -1;
            this.view.MedicamentLabelValue.Text = "-";
            this.view.NameTxtBox.Text = "";
            this.view.DescriptionTxtBox.Text = "";
            this.view.ExpirationDatePicker.Text = "";
            this.view.IngredientsTxtBox.Text = "";
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (this.view.SelectedMedicamentId < 1) {
                    await this.createMedicament(
                        this.view.NameTxtBox.Text,
                        this.view.DescriptionTxtBox.Text,
                        this.view.ExpirationDatePicker.Value.ToString(DateTimeFormats.MYSQL_DATE),
                        this.view.IngredientsTxtBox.Text
                    );
                } else {
                    await this.updateMedicament(
                        this.view.SelectedMedicamentId,
                        this.view.NameTxtBox.Text,
                        this.view.DescriptionTxtBox.Text,
                        this.view.ExpirationDatePicker.Value.ToString(DateTimeFormats.MYSQL_DATE),
                        this.view.IngredientsTxtBox.Text
                    );
                }

                this.handleResetButton();
                this.init();

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<long> createMedicament(
            string name, 
            string description, 
            string expirationDate, 
            string ingredients
        ) {
            try {
                long id = await medicamentModel.createMedicament(
                    name, 
                    description,
                    expirationDate,
                    ingredients
                );

                MessageBox.Show("Medikamenti u shtua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return id;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return -1;
            }
        }

        private async Task<long> updateMedicament(
            long id, 
            string name, 
            string description, 
            string expirationDate, 
            string ingredients
        ) {
            try {
                long updatedId = await medicamentModel.updateMedicament(
                    id,
                    name,
                    description,
                    expirationDate,
                    ingredients
                );
                
                MessageBox.Show("Medikamenti u përditësua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return updatedId;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return -1;
            }
        }
    }
}

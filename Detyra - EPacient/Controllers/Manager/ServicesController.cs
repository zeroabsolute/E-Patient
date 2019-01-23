using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class ServicesController {
        private Services view;

        private Models.Service serviceModel;

        public ServicesController(Services view) {
            this.view = view;
            this.serviceModel = new Models.Service();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read services from DB and populate table
                List<Models.Service> services = await this.serviceModel.readServices();

                this.view.ServicesTable.DataGrid.Rows.Clear();
                this.view.ServicesTable.DataGrid.Refresh();

                services.ForEach((item) => {
                    this.view.ServicesTable.DataGrid.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Fee,
                        item.Description
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

                var selectedRow = this.view.ServicesTable.DataGrid.SelectedRows.Count > 0
                ? this.view.ServicesTable.DataGrid.SelectedRows[0]
                : null;

                if (selectedRow != null) {
                    int id = (int) selectedRow.Cells[0].Value;
                    string name = selectedRow.Cells[1].Value.ToString();
                    string fee = selectedRow.Cells[2].Value.ToString();
                    string description = selectedRow.Cells[3].Value.ToString();

                    this.view.SelectedService = name;
                    this.view.SelectedServiceId = id;
                    this.view.ServiceLabelValue.Text = name;
                    this.view.NameTxtBox.Text = name;
                    this.view.FeeTxtBox.Text = fee;
                    this.view.DescriptionTxtBox.Text = description;
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
            this.view.SelectedService = null;
            this.view.SelectedServiceId = -1;
            this.view.ServiceLabelValue.Text = "-";
            this.view.NameTxtBox.Text = "";
            this.view.FeeTxtBox.Text = "";
            this.view.DescriptionTxtBox.Text = "";
        }

        /**
         * Controller to handle submit button
         */

        public async void handleSubmitButton() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (this.view.SelectedServiceId < 1) {
                    await this.createService(
                        this.view.NameTxtBox.Text,
                        int.Parse(this.view.FeeTxtBox.Text),
                        this.view.DescriptionTxtBox.Text
                    );
                } else {
                    await this.updateService(
                        this.view.SelectedServiceId,
                        this.view.NameTxtBox.Text,
                        int.Parse(this.view.FeeTxtBox.Text),
                        this.view.DescriptionTxtBox.Text
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

        private async Task<long> createService(string name, int fee, string description) {
            try {
                long id = await serviceModel.createService(
                    name,
                    fee,
                    description
                );

                MessageBox.Show("Shërbimi u shtua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return id;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return -1;
            }
        }

        private async Task<long> updateService(long id, string name, int fee, string description) {
            try {
                long updatedId = await serviceModel.updateService(
                    id,
                    name,
                    fee,
                    description
                );
                
                MessageBox.Show("Shërbimi u përditësua me sukses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return updatedId;
            } catch (Exception e) {
                string caption = "Problem në shkrim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return -1;
            }
        }
    }
}

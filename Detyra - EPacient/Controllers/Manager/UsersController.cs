﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class UsersController {
        private Users view;

        private Models.Role roleModel;
        private Models.Operator operatorModel;

        public UsersController(Users view) {
            this.view = view;
            this.roleModel = new Models.Role();
        }

        /**
         * Controller to handle initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read roles from DB and populate combobox
                List<Models.Role> roles = await roleModel.readRoles();
                this.view.CBox.comboBox.DisplayMember = "name";
                this.view.CBox.comboBox.ValueMember = "id";
                this.view.CBox.comboBox.DataSource = roles;

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

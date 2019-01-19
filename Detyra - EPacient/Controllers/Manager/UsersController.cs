using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Models;
using Detyra___EPacient.Views.Operator;

namespace Detyra___EPacient.Controllers.Manager {
    class UsersController {
        private Users view;
        private Role roleModel;

        public UsersController(Users view) {
            this.view = view;
            this.roleModel = new Role();
        }

        /**
         * Controller to handle initial data
         */

        public async void init() {
            Cursor.Current = Cursors.WaitCursor;

            // Read roles from DB and populate combobox
            List<Role> roles = await roleModel.readRoles();
            this.view.CBox.comboBox.DisplayMember = "name";
            this.view.CBox.comboBox.ValueMember = "id";
            this.view.CBox.comboBox.DataSource = roles;


            Cursor.Current = Cursors.Arrow;
        }


    }
}

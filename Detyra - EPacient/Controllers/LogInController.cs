using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Detyra___EPacient.Models;

namespace Detyra___EPacient.Controllers {
    class LogInController {
        public LogInController() {

        }

        public void logIn(string email, string password) {
            User userModel = new User();
            userModel.userLogIn(email, password);
        }
    }
}

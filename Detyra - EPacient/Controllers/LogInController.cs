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

        public User logIn(string email, string password) {
            try {
                User userModel = new User();
                User loggedInUser = userModel.userLogIn(email, password);

                Console.WriteLine(loggedInUser.toString());

                return loggedInUser;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detyra___EPacient.Helpers {
    class Validators {
        /**
         * Email validator
         */
        
        public static bool validateEmail(string email) {
            try {
                if (email == null || email == "") {
                    return false;
                }

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch (Exception e) {
                return false;
            }
        }

        /**
         * Password validator
         */
        
        public static bool validatePassword(string password) {
            if (password == null || password.Length < 6) {
                return false;
            } else {
                return true;
            }
        }
    }
}

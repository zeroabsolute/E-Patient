using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detyra___EPacient.Models {
    class Employee {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }

        public Employee() {
            this.Id = -1;
            this.FirstName = "";
            this.LastName = "";
            this.PhoneNumber = "";
            this.Address = "";
            this.DateOfBirth = new DateTime();
            this.User = null;
        }

        public Employee(
            int id,
            string firstName,
            string lastName,
            string phoneNumber,
            string address,
            DateTime dob,
            User user
        ) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateOfBirth = dob;
            this.User = user;
        }
    }
}

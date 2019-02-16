using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detyra___EPacient.Models {
    public class Status {
        public int Id { get; set; }
        public string Name { get; set; }

        public Status(int id, string name) {
            this.Id = id;
            this.Name = name;
        }
    }
}

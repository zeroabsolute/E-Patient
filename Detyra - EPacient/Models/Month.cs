namespace Detyra___EPacient.Models {
    class Month {
        public string Name { get; set; }
        public string Value { get; set; }

        public Month(string name, string value) {
            this.Name = name;
            this.Value = value;
        }
    }
}

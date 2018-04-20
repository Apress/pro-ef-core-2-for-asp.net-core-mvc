using System.Collections.Generic;

namespace DataApp.Models {

    public class Supplier {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public ContactDetails Contact { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

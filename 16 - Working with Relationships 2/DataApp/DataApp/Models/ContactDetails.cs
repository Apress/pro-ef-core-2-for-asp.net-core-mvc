namespace DataApp.Models {

    public class ContactDetails {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ContactLocation Location { get; set; }

        public long? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}

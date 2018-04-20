namespace AdvancedApp.Models {

    public class Employee {

        public long Id { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public decimal Salary { get; set; }

        public SecondaryIdentity OtherIdentity { get; set; }
    }
}

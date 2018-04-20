namespace AdvancedApp.Models {

    public class SecondaryIdentity {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool InActiveUse { get; set; }

        public string PrimarySSN { get; set; }
        public string PrimaryFamilyName { get; set; }
        public string PrimaryFirstName { get; set; }
        public Employee PrimaryIdentity { get; set; }
    }
}

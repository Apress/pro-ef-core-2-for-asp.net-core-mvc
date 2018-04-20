namespace ExistingDb.Models.Manual {

    public class ShoeCategoryJunction {

        public long Id { get; set; }
        public long ShoeId { get; set; }
        public long CategoryId { get; set; }

        public Category Category { get; set; }
        public Shoe Shoe { get; set; }
    }
}

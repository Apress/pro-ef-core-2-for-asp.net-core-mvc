using System.Collections.Generic;

namespace ExistingDb.Models.Manual {

    public class Category {
        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ShoeCategoryJunction> Shoes { get; set; }
    }
}

using System.Collections.Generic;

namespace ExistingDb.Models.Manual {

    public class ShoeWidth {

        public long UniqueIdent { get; set; }

        public string WidthName { get; set; }

        public IEnumerable<Shoe> Products { get; set; }
    }
}

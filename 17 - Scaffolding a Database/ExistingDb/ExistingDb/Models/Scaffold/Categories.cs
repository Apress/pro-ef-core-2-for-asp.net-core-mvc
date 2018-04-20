using System;
using System.Collections.Generic;

namespace ExistingDb.Models.Scaffold
{
    public partial class Categories
    {
        public Categories()
        {
            ShoeCategoryJunction = new HashSet<ShoeCategoryJunction>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<ShoeCategoryJunction> ShoeCategoryJunction { get; set; }
    }
}

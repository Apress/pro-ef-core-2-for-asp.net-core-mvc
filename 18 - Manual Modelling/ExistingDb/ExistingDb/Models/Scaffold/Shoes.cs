using System;
using System.Collections.Generic;

namespace ExistingDb.Models.Scaffold
{
    public partial class Shoes
    {
        public Shoes()
        {
            ShoeCategoryJunction = new HashSet<ShoeCategoryJunction>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long ColorId { get; set; }
        public decimal Price { get; set; }
        public long? FittingId { get; set; }

        public Colors Color { get; set; }
        public Fittings Fitting { get; set; }
        public SalesCampaigns SalesCampaigns { get; set; }
        public ICollection<ShoeCategoryJunction> ShoeCategoryJunction { get; set; }
    }
}

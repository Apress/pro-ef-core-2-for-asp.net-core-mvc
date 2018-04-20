using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExistingDb.Models.Manual {

    [Table("SalesCampaigns")]
    public class SalesCampaign {

        public long Id { get; set; }
        public string Slogan { get; set; }
        public int? MaxDiscount { get; set; }
        public DateTime? LaunchDate { get; set; }

        public long ShoeId { get; set; }
        public Shoe Shoe { get; set; }
    }
}

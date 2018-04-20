namespace DataApp.Models {

    public class ProductShipmentJunction {

        public long Id { get; set; }
	
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}

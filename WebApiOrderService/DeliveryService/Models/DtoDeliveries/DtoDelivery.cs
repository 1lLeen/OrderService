namespace DeliveryService.Models.DtoDeliveries
{
    public class DtoDelivery
    {
        public int DeliveryId { get; set; }
        public string DeliveryOrderName { get; set; }
        public string DeliveryFrom { get; set; }
        public string DeliveryTo { get; set; }
        public DateTime DeliveryDeadyline { get; set; }
        public int DeliveryPriceDelivery { get; set; }
    }
}

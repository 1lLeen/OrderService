namespace DeliveryService.Models.DeliveriesModels
{
    public class Delivery
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DeadLine { get; set; }
        public int PriceDelivery { get; set; }
    }
}

namespace WebApiOrderService.Models.DtoOrders
{
    public class DtoOrder
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string OrderDescription { get; set; }
        public int OrderPrice { get; set; }

    }
}

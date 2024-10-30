using WebApiOrderService.Models.DtoOrders;

namespace WebApiOrderService.Models.ConsumerModels
{
    public class CreateOrder
    {
        public string Name { get; set; }
        public string Price { get; set; } 
    }
}

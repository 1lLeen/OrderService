using WebApiOrderService.Events.EventsInterfaces;

namespace WebApiOrderService.Events.EventsModels
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsCreated { get; set; }


    }
}

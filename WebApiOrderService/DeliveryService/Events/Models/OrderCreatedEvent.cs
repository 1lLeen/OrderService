using DeliveryService.Events.Interfaces;

namespace DeliveryService.Events.Models
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public Guid OrderId { get; set; }

        public bool IsCreated { get; set; }
    }
}

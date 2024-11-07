namespace DeliveryService.Events.Interfaces
{
    public interface IOrderCreatedEvent
    {
        Guid OrderId { get; }
        bool IsCreated { get; set; }
    }
}

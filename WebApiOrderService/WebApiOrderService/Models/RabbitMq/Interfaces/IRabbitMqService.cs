namespace WebApiOrderService.Models.RabbitMq.Interfaces
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}

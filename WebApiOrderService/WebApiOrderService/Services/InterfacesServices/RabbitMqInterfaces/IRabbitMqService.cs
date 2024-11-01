namespace WebApiOrderService.Services.InterfacesServices.RabbitMqInterfaces
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}

using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using WebApiOrderService.Models.RabbitMq.Interfaces;

namespace WebApiOrderService.Models.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queueName = "GetMessageOrder";

                    byte[] body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                }
            }
           
            Console.WriteLine($"Sent: {message}");
        }
    }
}

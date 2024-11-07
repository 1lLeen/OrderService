using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace DeliveryService.Services.RabbitMqService
{
    public abstract class RabbitSubscriber
    {
        public string GetMessage(string queueName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            queueName = "Order";
            channel.QueueBind(queue: queueName,
                              exchange: "logs",
                              routingKey: string.Empty);
             
            var consumer = new EventingBasicConsumer(channel);
            var message = "";
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
            };
            channel.BasicConsume(queue: queueName,

                                 consumer: consumer);

            return message;
        }
    }
}

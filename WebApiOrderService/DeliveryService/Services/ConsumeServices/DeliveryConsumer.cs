using DeliveryService.Events.Interfaces;
using DeliveryService.Models.DtoDeliveries;
using MassTransit;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace DeliveryService.Services.ConsumeServices
{
    public class DeliveryConsumer : IConsumer<DtoDelivery>
    {
        ILogger<DeliveryConsumer> _logger;
        public DeliveryConsumer(ILogger<DeliveryConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<DtoDelivery> context) 
        {
            _logger.LogInformation("Value: {Value}", context.Message.DeliveryId);
            return Task.CompletedTask;
        }
    }
}

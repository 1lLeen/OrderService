using MassTransit;

namespace WebApiOrderService.Models.ConsumerModels
{
    public class CreateOrderConsumer : IConsumer<CreateOrder>
    {
        private readonly ILogger<CreateOrderConsumer> _logger;
        public CreateOrderConsumer(ILogger<CreateOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            _logger.LogInformation($"Consume order was created: {context.Message.Name} : {context.Message.Price}");
        }
    }
}

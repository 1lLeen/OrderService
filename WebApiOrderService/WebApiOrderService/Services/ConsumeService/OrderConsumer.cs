using MassTransit;
using System.Security.Principal;
using WebApiOrderService.Models.DtoOrders;

namespace WebApiOrderService.Services.ConsumeService
{
    public class OrderConsumer : IConsumer<DtoOrder>
    {
        private readonly ILogger<OrderConsumer> _logger;
        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<DtoOrder> context)
        {
            _logger.LogInformation($"Order was created {context.Message.OrderName}");
            return Task.CompletedTask;
        }
    }
}

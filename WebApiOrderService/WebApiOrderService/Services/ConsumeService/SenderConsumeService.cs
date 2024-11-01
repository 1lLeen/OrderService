using MassTransit;
using System.Security.Principal;
using WebApiOrderService.Models.DtoOrders;

namespace WebApiOrderService.Services.ConsumeService
{
    public class SenderConsumeService : IConsumer<DtoOrder>
    {
        public Task Consume(ConsumeContext<DtoOrder> context)
        {
            Console.WriteLine($"Order was created {context.Message.OrderName}");
            return Task.CompletedTask;
        }
    }
}

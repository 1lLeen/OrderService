using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services;
using WebApiOrderService.Services.InterfacesServices.OrderInterfaces;
using WebApiOrderService.Services.InterfacesServices.RabbitMqInterfaces;

namespace WebApiOrderService.Contorllers
{
    [Route("/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        readonly IOrderService orderService;
        readonly IMapper _mapper; 
        private readonly IRabbitMqService _mqService; 
        private readonly IBus _bus;
        private readonly IRequestClient<DtoOrder> _client;
        public OrderController(IOrderService orderService, IMapper mapper, IRabbitMqService mqService, IBus bus, IRequestClient<DtoOrder> client)
        {
            this.orderService = orderService;
            _mapper = mapper;
            _mqService = mqService; 
            _bus = bus;
            _client = client;
        }
        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult<IEnumerable<DtoOrder>>> GetAllOrders()
        { 
            return await orderService.GetAllOrders();
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<DtoOrder>> GetOrder(int id)
        {
            return await orderService.GetOrderById(id);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult<List<DtoOrder>>> CreateOrder(DtoOrder order)
        {
            if (order != null)
            { 
                var result = await orderService.AddOrder(order);
                //_mqService.SendMessage($"Order was created {order.OrderName}");
                //var url = new Uri("rabbitmq://localhost/SenderConsumeService");

                //var endpoint = await _bus.GetSendEndpoint(url);
                await _bus.Publish(result.LastOrDefault());
                //await endpoint.Send(result.LastOrDefault());
                return result;
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<DtoOrder>> DeleteOrder(int id)
        { 
            if(id > 0)
            {
                return await orderService.DeleteOrderById(id);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<DtoOrder>> UpdateOrder(int id, DtoOrder order)
        { 
            if (order != null)
            { 
                return await orderService.UpdateOrder(id, order);
            }
            return BadRequest();
        }
    }
}

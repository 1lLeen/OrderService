using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services.InterfacesServices;

namespace WebApiOrderService.Contorllers
{
    public class OrderController : ControllerBase
    {
        readonly IOrderService orderRepository;
        readonly IMapper _mapper;
        public OrderController(IOrderService orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _mapper = mapper;
        }
        [HttpGet("/api/GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            if(orderRepository == null)
            {
                return NotFound();
            }
            List<Order> orders = await orderRepository.GetAllOrders();
            return orders;
        }

        [HttpGet("/api/GetOrder/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            return await orderRepository.GetOrder(id);
        }

        [HttpPost("/api/Create")]
        public async Task<ActionResult<List<Order>>> Create(Order order)
        {
            if(order != null)
            {
                orderRepository.PostOrder(_mapper.Map<DtoOrder>(order));
                return await orderRepository.GetAllOrders();
            }
            return BadRequest();
        }
        [HttpDelete("/api/Delete/{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            var order = await orderRepository.GetOrder(id);
            if(order != null)
            {
                orderRepository.DeleteOrder(id);
                return order;
            }
            return BadRequest();
        }
        [HttpPut("/api/Put/{id}")]
        public async Task<ActionResult<Order>> Update(int id, Order order)
        { 
            if (order != null)
            {
                var orderToUpdate = await orderRepository.GetOrder(id);
                orderToUpdate.Name = order.Name;
                orderToUpdate.Description = order.Description;
                orderToUpdate.Price = order.Price;

                orderRepository.PutOrder(_mapper.Map<DtoOrder>(orderToUpdate));
                return order;
            }
            return BadRequest();
        }
    }
}

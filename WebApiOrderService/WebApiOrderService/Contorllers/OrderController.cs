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
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            if(orderRepository == null)
            {
                return NotFound();
            }
            List<Order> orders = orderRepository.GetAllOrders();
            return orders;
        }

        [HttpGet("/api/GetOrder/{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            return orderRepository.GetOrder(id);
        }

        [HttpPost("/api/Create")]
        public ActionResult<List<Order>> Create([Bind("Name,Description,Price")] Order order)
        {
            if(order != null)
            {
                orderRepository.PostOrder(_mapper.Map<DtoOrder>(order));
                return orderRepository.GetAllOrders();
            }
            return BadRequest();
        }
        [HttpDelete("/api/Delete/{id}")]
        public ActionResult<Order> Delete(int id)
        {
            var order = orderRepository.GetOrder(id);
            if(order != null)
            {
                orderRepository.DeleteOrder(id);
                return order;
            }
            return BadRequest();
        }
        [HttpPut("/api/Put/{id}")]
        public ActionResult<Order> Update(int id,[Bind("Name,Description,Price")] Order order)
        { 
            if (order != null)
            {
                var orderToUpdate = orderRepository.GetOrder(id);
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

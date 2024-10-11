using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Repositories.InterfacesRepositories;

namespace WebApiOrderService.Contorllers
{
    public class OrderController : ControllerBase
    {
        readonly IOrderRepository orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [HttpGet("/api/GetAllOrders")]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            if(orderRepository == null)
            {
                return NotFound();
            }
            return orderRepository.GetAllOrders();
        }

        [HttpGet("/api/GetOrder/{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            return orderRepository.GetOrder(id);
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if(order != null)
            {
                orderRepository.PostOrder(order);
                if(orderRepository.GetAllOrders().LastOrDefault() == order)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
 
    }
}

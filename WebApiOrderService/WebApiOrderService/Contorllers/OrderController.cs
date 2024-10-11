using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Repositories.InterfacesRepositories;

namespace WebApiOrderService.Contorllers
{
    public class OrderController : ControllerBase
    {
        readonly IOrderRepository orderRepository;
        readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            _mapper = mapper;
        }
        [HttpGet("/api/GetAllOrders")]
        public ActionResult<IEnumerable<DtoOrder>> GetAllOrders()
        {
            if(orderRepository == null)
            {
                return NotFound();
            }
            List<DtoOrder> dtoOrder = _mapper.Map<List<DtoOrder>>(orderRepository.GetAllOrders());
            return dtoOrder;
        }

        [HttpGet("/api/GetOrder/{id}")]
        public ActionResult<DtoOrder> GetOrder(int id)
        {
            return _mapper.Map<DtoOrder>(orderRepository.GetOrder(id));
        }

        [HttpPost("/api/GetOrder/{order}")]
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

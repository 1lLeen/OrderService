using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services;
using WebApiOrderService.Services.InterfacesServices;

namespace WebApiOrderService.Contorllers
{
    [Route("/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        readonly IOrderService orderService;
        readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            _mapper = mapper;
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
        public async Task<ActionResult<List<DtoOrder>>> Create(DtoOrder order)
        {
            if (order != null)
            { 
                return await orderService.AddOrder(order);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<DtoOrder>> Delete(int id)
        { 
            if(id > 0)
            {
                return await orderService.DeleteOrderById(id);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<DtoOrder>> Update(int id, DtoOrder order)
        { 
            if (order != null)
            { 
                return await orderService.UpdateOrder(id, order);
            }
            return BadRequest();
        }
    }
}

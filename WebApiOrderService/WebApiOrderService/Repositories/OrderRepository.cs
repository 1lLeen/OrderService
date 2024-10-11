using AutoMapper;
using WebApiOrderService.EF;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Repositories.InterfacesRepositories;

namespace WebApiOrderService.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly OrderDbContext _context; 
        private readonly IMapper _mapper;
        public OrderRepository(OrderDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void DeleteOrder(int id)
        {
            if(id > 0)
            {
                var order = GetOrder(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void PostOrder(DtoOrder order)
        {
            if(order != null)
            {
                if (!ExistsOrder(order))
                { 
                    _context.Orders.Add(_mapper.Map<Order>(order));
                    _context.SaveChanges();
                }
            }
        }

        public void PutOrder(DtoOrder order)
        {
            if (ExistsOrder(order)) 
            {
                _context.Orders.Entry(_mapper.Map<Order>(order));
                _context.SaveChanges();
            }
        }
        private bool ExistsOrder(DtoOrder order)
        {
            return _context.Orders.Any(o => o.Id == order.OrderId);
        }
    }
}

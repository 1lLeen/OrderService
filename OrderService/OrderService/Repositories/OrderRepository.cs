using OrderService.DbContext;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : InterfacesRepository.IOrderRepository
    {
        private readonly OrderContext _context;
        public OrderRepository(OrderContext context) 
        {
            _context = context;
        }
        public bool ExistsOrder(Order order)
        {
            return _context.Orders.Any(o=> o.Id == order.Id);
        }
        public void AddOrder(Order order)
        {
            if(order != null)
            {
                if (!ExistsOrder(order))
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteOrder(int id)
        {
            if(id < 0)
            {
                var order = _context.Orders.Find(id);
                if(order != null)
                {
                    _context.Remove(order);
                    _context.SaveChanges();
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

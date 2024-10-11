using WebApiOrderService.EF;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Repositories.InterfacesRepositories;

namespace WebApiOrderService.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        public OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context;
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

        public void PostOrder(Order order)
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

        public void PutOrder(Order order)
        {
            if (ExistsOrder(order)) 
            {
                _context.Orders.Entry(order);
                _context.SaveChanges();
            }
        }
        private bool ExistsOrder(Order order)
        {
            return _context.Orders.Any(o => o.Id == order.Id);
        }
    }
}

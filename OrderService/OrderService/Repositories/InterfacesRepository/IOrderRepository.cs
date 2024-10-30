using OrderService.Models;

namespace OrderService.Repositories.InterfacesRepository
{
    public interface IOrderRepository
    {
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}

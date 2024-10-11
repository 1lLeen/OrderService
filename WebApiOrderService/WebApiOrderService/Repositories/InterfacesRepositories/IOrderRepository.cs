using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.Repositories.InterfacesRepositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrder(int id);
        void PostOrder(Order order);
        void PutOrder(Order order);
        void DeleteOrder(int id);

    }
}

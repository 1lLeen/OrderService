using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.Repositories.InterfacesRepositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrder(int id);
        void PostOrder(DtoOrder order);
        void PutOrder(DtoOrder order);
        void DeleteOrder(int id);

    }
}

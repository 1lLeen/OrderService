using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.Services.InterfacesServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        void AddOrder(DtoOrder order);
        void UpdateOrder(DtoOrder order);
        void DeleteOrderById(int id);
        void DeleteAllOrders();

    }
}

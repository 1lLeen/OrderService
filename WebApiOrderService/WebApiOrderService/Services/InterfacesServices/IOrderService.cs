using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.Services.InterfacesServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrder(int id);
        void PostOrder(DtoOrder order);
        void PutOrder(DtoOrder order);
        void DeleteOrder(int id);
        void DeleteAllOrders();

    }
}

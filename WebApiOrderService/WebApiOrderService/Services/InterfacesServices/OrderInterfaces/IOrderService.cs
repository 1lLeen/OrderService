using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.Services.InterfacesServices.OrderInterfaces
{
    public interface IOrderService
    {
        Task<List<DtoOrder>> GetAllOrders();
        Task<DtoOrder> GetOrderById(int id);
        Task<List<DtoOrder>> AddOrder(DtoOrder order);
        Task<DtoOrder> UpdateOrder(int id, DtoOrder order);
        Task<DtoOrder> DeleteOrderById(int id);
        void DeleteAllOrders();

    }
}

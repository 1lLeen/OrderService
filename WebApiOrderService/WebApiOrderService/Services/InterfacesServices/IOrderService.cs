using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.Services.InterfacesServices
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrder(int id);
        void PostOrder(DtoOrder order);
        void PutOrder(DtoOrder order);
        void DeleteOrder(int id);

    }
}

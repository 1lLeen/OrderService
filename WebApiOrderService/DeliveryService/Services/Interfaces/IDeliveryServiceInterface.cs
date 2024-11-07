using DeliveryService.Models.DtoDeliveries;

namespace DeliveryService.Services.Interfaces
{
    public interface IDeliveryServiceInterface
    {
        Task<List<DtoDelivery>> GetAllDeliveries();
        Task<DtoDelivery> GetDeliveryById(int id);

        Task<DtoDelivery> CreateDelivery(DtoDelivery delivery);
    }
}

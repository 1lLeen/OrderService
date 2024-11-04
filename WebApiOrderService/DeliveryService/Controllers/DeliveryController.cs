using DeliveryService.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using DeliveryService.Events.Interfaces;
using DeliveryService.Services.Interfaces;
using DeliveryService.Services.ConsumeServices;
using DeliveryService.Events.Models;
using DeliveryService.Models.DtoDeliveries;

namespace DeliveryService.Controllers
{
    [Route("/[controller]/[action]")]
    public class DeliveryController : ControllerBase
    {
    
        private readonly IDeliveryServiceInterface _services;
        private readonly IBus _bus;
        public DeliveryController(IDeliveryServiceInterface services, IBus bus)
        {
            _services = services;
            _bus = bus;
        }
        [Route("/[controller]/[action]/{id}")]
        public async Task<DtoDelivery> GetDeliveryById(int id)
        {
            return await _services.GetDeliveryById(id);
        }
        [Route("/[controller]/[action]")]
        public async Task<List<DtoDelivery>> GetAllDelivries()
        {
            return await _services.GetAllDeliveries();
        }
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> CreateDelivery(DtoDelivery delivery) 
        {
            await _services.CreateDelivery(delivery);
            return Ok();
        }

    }
}

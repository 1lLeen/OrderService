using AutoMapper;
using DeliveryService.AutoMapperDelivery;
using DeliveryService.EF;
using DeliveryService.Events.Interfaces;
using DeliveryService.Models.DeliveriesModels;
using DeliveryService.Models.DtoDeliveries;
using DeliveryService.Services.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services
{
    public class DelService: IDeliveryServiceInterface
    {
        private readonly DeliveryDbContext _context;
        private readonly IBus _bus;
        private readonly IRequestClient<DtoDelivery> _client;
        private readonly IMapper _mapper;
        public DelService(IBus bus, IRequestClient<DtoDelivery> client, IMapper mapper, DeliveryDbContext context)
        {
            _bus = bus;
            _client = client;
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<DtoDelivery>> GetAllDeliveries()
        {
            if (_context.deliveries != null)
            {
                var lstDeliveries = await _context.deliveries.ToListAsync();
                return _mapper.Map<List<DtoDelivery>>(lstDeliveries);
            }
            else
            {
                throw new ArgumentException("List is null");
            }
        }
        public async Task<DtoDelivery> GetDeliveryById(int id)
        {
            var order = await _context.deliveries.Where(o => o.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (order != null)
            {
                return _mapper.Map<DtoDelivery>(order);
            }
            else
            {
                throw new ArgumentException("This order not exists");
            }
        }
        public async Task<DtoDelivery> CreateDelivery(DtoDelivery delivery)
        {
          
            if (delivery != null)
            {
                var lst = await GetAllDeliveries();
                if (!lst.Contains(delivery))
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        await _context.AddAsync(_mapper.Map<Delivery>(delivery));
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        await _bus.Publish(delivery);
                        return delivery;
                    }
                }
                throw new ArgumentException("Delivery is exists");
            }
            throw new ArgumentException("uncorrect information");
        }
    }
}

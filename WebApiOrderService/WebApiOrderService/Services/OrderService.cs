using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiOrderService.EF;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services.InterfacesServices;

namespace WebApiOrderService.Services
{
    public class OrderService:IOrderService
    {
        private readonly OrderDbContext _context; 
        private readonly IMapper _mapper;
        public OrderService(OrderDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void DeleteOrder(int id)
        {
            if(id > 0)
            {
                var order = await GetOrder(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            if(_context.Orders != null)
            {
                return await _context.Orders.ToListAsync();
            }
            else
            {
                throw new ArgumentException("List is null");
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if(order != null)
            {
                return order;
            }
            else
            {
                throw new ArgumentException("This order not exists");
            }
        }

        public async void PostOrder(DtoOrder order)
        {
            if(order != null)
            {
                if (await  ExistsOrder(order) == true)
                { 
                    await _context.Orders.AddAsync(_mapper.Map<Order>(order));
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async void PutOrder(DtoOrder order)
        {
            if (await ExistsOrder(order) == true) 
            {
                _context.Orders.Entry(_mapper.Map<Order>(order));
                await _context.SaveChangesAsync();
            }
        }
        private async Task<bool> ExistsOrder(DtoOrder order)
        {
            return await _context.Orders.AnyAsync(o => o.Id == order.OrderId);
        }
        public async void DeleteAllOrders()
        {
            var orders = await GetAllOrders();
            if(orders != null)
            {
                foreach(var item in orders)
                {
                    DeleteOrder(item.Id);
                }
            }
            else
            {
                throw new ArgumentException("Dont have any orders");
            }
        }
    }
}

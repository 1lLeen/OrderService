using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Transactions;
using System.Xml;
using WebApiOrderService.EF;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services.InterfacesServices.OrderInterfaces;
using static MassTransit.ValidationResultExtensions;

namespace WebApiOrderService.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        public OrderService(OrderDbContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<DtoOrder> DeleteOrderById(int id)
        {
            if (id > 0)
            {
                var order = await GetOrderById(id);
                if (ExistsOrder(order))
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        _context.Orders.Remove(_mapper.Map<Order>(order));
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return order;
                    }
                }
                else
                {
                    throw new ArgumentException("Order is not exists");
                }
            }
            else
            {
                throw new ArgumentException("ID not correct");
            }
        }

        public async Task<List<DtoOrder>> GetAllOrders()
        {
            if (_context.Orders != null)
            {
                var lstOrders = await _context.Orders.ToListAsync();
                return _mapper.Map<List<DtoOrder>>(lstOrders);
            }
            else
            {
                throw new ArgumentException("List is null");
            }
        }

        public async Task<DtoOrder> GetOrderById(int id)
        {
            var order = await _context.Orders.Where(o => o.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (order != null)
            {
                return _mapper.Map<DtoOrder>(order);
            }
            else
            {
                throw new ArgumentException("This order not exists");
            }
        }

        public async Task<List<DtoOrder>> AddOrder(DtoOrder order)
        {
            if (order != null)
            {
                if (!ExistsOrder(order))
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        await _context.AddAsync(_mapper.Map<Order>(order));
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        await _bus.Publish(order);
                        return await GetAllOrders();
                    }
                }
                else
                {
                    throw new ArgumentException("Order is exists");
                }
            }
            else
            {
                throw new ArgumentException("Order is not correct");
            }
        }

        public async Task<DtoOrder> UpdateOrder(int id, DtoOrder order)
        {

            var orderToUpdate = await GetOrderById(id);
            if (orderToUpdate != null)
            {
                if (ExistsOrder(orderToUpdate))
                {
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        orderToUpdate.OrderName = order.OrderName;
                        orderToUpdate.OrderDescription = order.OrderDescription;
                        orderToUpdate.OrderPrice = order.OrderPrice;
                        _context.Orders.Entry(_mapper.Map<Order>(orderToUpdate)).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return orderToUpdate;
                    }
                }
            }
            throw new ArgumentException("This order not exists with this id");
        }
        private bool ExistsOrder(DtoOrder order)
        {
            return _context.Orders.Any(o =>
            o.Name == order.OrderName &&
            o.Description == order.OrderDescription &&
            o.Price == order.OrderPrice);
        }
        public async void DeleteAllOrders()
        {
            var orders = await GetAllOrders();
            if (orders != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Orders.RemoveRange(_mapper.Map<List<Order>>(orders));
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new ArgumentException($"{nameof(GetAllOrders)}");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Dont have any orders");
            }
        }

    }
}

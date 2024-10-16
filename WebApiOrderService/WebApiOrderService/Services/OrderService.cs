using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction _dbContextTransaction; 
        public OrderService(OrderDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void DeleteOrder(int id)
        {
            if (id > 0)
            {
                _dbContextTransaction = await _context.Database.BeginTransactionAsync();
                _dbContextTransaction.CreateSavepoint("BeforeDelete");
                var order = await GetOrder(id);
                if (order != null)
                {
                    try
                    {
                        _context.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                        _dbContextTransaction.Commit();
                    }
                    catch
                    {
                        _dbContextTransaction.RollbackToSavepoint("BeforeMoreBlogs");
                    }

                }
                else
                {
                    throw new ArgumentException("Order is not exists");
                }
            }
            else 
            {
                throw new ArgumentException("Id is not correct");
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {

            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
            if (_context.Orders != null)
            {
                var lstOrders = await _context.Orders.ToListAsync();
                await _dbContextTransaction.CommitAsync();
                return lstOrders;
            }
            else
            {
                throw new ArgumentException("List is null");
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if(order != null)
            {
                await _dbContextTransaction.CommitAsync();
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
                _dbContextTransaction = await _context.Database.BeginTransactionAsync();
                if (await ExistsOrder(order) == true)
                {
                    try
                    {
                        _dbContextTransaction.CreateSavepoint("BeforeAddOrder");
                        await _context.Orders.AddAsync(_mapper.Map<Order>(order));
                        await _context.SaveChangesAsync();
                        _dbContextTransaction?.Commit();
                    }
                    catch
                    {
                        _dbContextTransaction.RollbackToSavepoint("BeforeAddOrder");
                    }
                }
                else
                {
                    throw new ArgumentException("Order is exists");
                }
            }
            else
            {
                throw new ArgumentException("Order is null");
            }
        }

        public async void PutOrder(DtoOrder order)
        {
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
            if (await ExistsOrder(order) == true)
            {
                try
                {
                    await _dbContextTransaction.CreateSavepointAsync("BeforeUpdate");
                    _context.Orders.Entry(_mapper.Map<Order>(order));
                    await _context.SaveChangesAsync();
                    _dbContextTransaction?.Commit();
                }
                catch
                {
                    _dbContextTransaction.RollbackToSavepoint("BeforeUpdate");
                    throw new ArgumentException("Connot be update");
                }

            }
            else 
            { 
                throw new ArgumentException("Order is not exists");
            }
        }
        private async Task<bool> ExistsOrder(DtoOrder order)
        {
            return await _context.Orders.AnyAsync(o => o.Id == order.OrderId);
        }
        public async void DeleteAllOrders()
        {
            var orders = await GetAllOrders();
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
            if(orders != null)
            {
                try
                {
                    await _dbContextTransaction.CreateSavepointAsync("BefeoreAllDelete");
                    _context.Orders.RemoveRange(orders);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    await _dbContextTransaction.RollbackToSavepointAsync("BeforeAllDelete");
                    throw new ArgumentException($"{nameof(GetAllOrders)}");
                }
            }
            else
            {
                throw new ArgumentException("Dont have any orders");
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using WebApiOrderService.Models.OrderModels;

namespace WebApiOrderService.EF
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public OrderDbContext() { }
        public DbSet<Order> Orders{ get; set; }

    }
}

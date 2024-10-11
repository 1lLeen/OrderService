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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=orderdb;Username=postgres;Password=jangir");
        }
    }
}

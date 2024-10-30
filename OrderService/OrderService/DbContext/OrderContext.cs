using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Models;
using System;

namespace OrderService.DbContext
{
    public class OrderContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Order> Orders{ get; set; }
        public OrderContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=orderdb;Username=postgres;Password=jangir");
        }
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }

}

using DeliveryService.Models.DeliveriesModels;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.EF
{
    public class DeliveryDbContext:DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

        public DeliveryDbContext() { }
        public DbSet<Delivery> deliveries{ get; set; }
    }
}

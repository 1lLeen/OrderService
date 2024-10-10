using Microsoft.EntityFrameworkCore;
using WebApiOrderService.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Configure the HTTP request pipeline.

builder.Services.AddDbContextPool<OrderDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString(connectionString)));
app.UseHttpsRedirection();
app.Run();
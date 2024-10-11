using Microsoft.EntityFrameworkCore;
using WebApiOrderService;
using WebApiOrderService.AutoMapperOrder;
using WebApiOrderService.EF;
using WebApiOrderService.Repositories;
using WebApiOrderService.Repositories.InterfacesRepositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
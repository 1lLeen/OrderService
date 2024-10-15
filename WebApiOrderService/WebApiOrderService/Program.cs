using Microsoft.EntityFrameworkCore;
using WebApiOrderService;
using WebApiOrderService.AutoMapperOrder;
using WebApiOrderService.EF;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services;
using WebApiOrderService.Services.InterfacesServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderService, OrderService>();
 
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
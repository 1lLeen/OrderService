using MassTransit;
using Microsoft.EntityFrameworkCore;
using WebApiOrderService;
using WebApiOrderService.AutoMapperOrder;
using WebApiOrderService.EF;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using WebApiOrderService.Services;
using WebApiOrderService.Services.InterfacesServices;
using RabbitMQ;
using RabbitMQ.Client;
using System.Reflection;
using WebApiOrderService.Models.RabbitMq;
using WebApiOrderService.Models.RabbitMq.Interfaces;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMassTransit(x =>
{ 
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
    });
});
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderService, OrderService>();
 
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=GetAllOrders}");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
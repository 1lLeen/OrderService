using MassTransit;
using Microsoft.EntityFrameworkCore;
using WebApiOrderService;
using WebApiOrderService.AutoMapperOrder;
using WebApiOrderService.EF;
using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using RabbitMQ;
using RabbitMQ.Client;
using System.Reflection;
using WebApiOrderService.Services.OrderServices;
using WebApiOrderService.Services.InterfacesServices.OrderInterfaces;
using WebApiOrderService.Services.RabbitServices;
using WebApiOrderService.Services.InterfacesServices.RabbitMqInterfaces;
using WebApiOrderService.Models.RabbitModels;
using WebApiOrderService.Services.ConsumeService;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();
    var rabbConfiguration = builder.Configuration.GetSection(nameof(RabbitConfiguration)).Get<RabbitConfiguration>();

    x.UsingRabbitMq((context, cfg) =>
    {
       
        cfg.Host(rabbConfiguration.Host, c =>
        {
            c.Username(rabbConfiguration.UserName);
            c.Password(rabbConfiguration.Password);
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
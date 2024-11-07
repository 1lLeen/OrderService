using DeliveryService.AutoMapperDelivery;
using DeliveryService.EF;
using DeliveryService.Models.RabbitModels;
using DeliveryService.Services;
using DeliveryService.Services.ConsumeServices;
using DeliveryService.Services.Interfaces;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DeliveryDbContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMassTransit(x =>
{
    var rabbConfiguration = builder.Configuration.GetSection(nameof(RabbitConfiguration)).Get<RabbitConfiguration>();
    x.AddConsumer<DeliveryConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbConfiguration.Host, c =>
        {
            c.Username(rabbConfiguration.UserName);
            c.Password(rabbConfiguration.Password);
        });
    });
});

builder.Services.AddTransient<IDeliveryServiceInterface, DelService>();
builder.Services.AddScoped<IDeliveryServiceInterface, DelService>(); 
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var app = builder.Build();

app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Delivery}/{action=Index}");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


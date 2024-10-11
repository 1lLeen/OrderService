using Microsoft.EntityFrameworkCore;
using WebApiOrderService;
using WebApiOrderService.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();
app.Run();
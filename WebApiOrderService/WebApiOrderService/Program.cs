var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.Run();
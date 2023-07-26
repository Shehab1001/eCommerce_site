using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Inventory.API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<InventoryDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDBContext") ?? throw new InvalidOperationException("Connection string 'InventoryDBContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

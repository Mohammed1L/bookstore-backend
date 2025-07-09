using BookStoreBackend.Dtos;
using BookStoreBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookstoreDB.Data; 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Enables minimal & controller-based endpoints
builder.Services.AddSwaggerGen();           // Adds Swagger generation

// dependency injection for Identity
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(connectionString));
    builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AllowAngularDev");
app.UseAuthorization();
// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();              // Generates Swagger JSON
    app.UseSwaggerUI();           // Swagger UI at /swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Required to use [ApiController] controllers

app.Run();
using BookstoreDB.Data;
using Microsoft.EntityFrameworkCore;
using BookRepo; // Adjust this to your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // Enable Swagger endpoint discovery
builder.Services.AddSwaggerGen();              // Enable Swagger UI

// Configure the database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(connectionString));

// Register your generic repository for dependency injection
builder.Services.AddScoped(typeof(GenricRepoistry<>));

// Register the concrete BookRepoistry so it can be injected
builder.Services.AddScoped<BookRepoistry>();

// Configure CORS to allow your Angular app running on localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAngularDev");

// Enable Swagger only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
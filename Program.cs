using BookstoreDB.Data;
using Microsoft.EntityFrameworkCore;
using Repo;
using Middlewares;
using services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(connectionString));

// Register repositories and services
builder.Services.AddScoped(typeof(GenricRepoistry<>));
builder.Services.AddScoped<BookRepoistry>();
builder.Services.AddScoped<OrderRepo>();
builder.Services.AddScoped<AuthRepo>();
builder.Services.AddScoped<TokenServices>();

// JWT configuration
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    // Log validation failures
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("JWT Authentication Failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("JWT Challenge Triggered: " + context.ErrorDescription);
            return Task.CompletedTask;
        }
    };
});

// Authorization
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

// Configure CORS for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", policy =>
    {
        policy.WithOrigins("https://bookstore-frontend-lemon-three.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddHttpClient<GeoService>();
var app = builder.Build();
app.UseRouting();           // If present (minimal APIs sometimes require it)


// Enable CORS
app.UseCors("AllowAny");

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enable authentication and authorization
app.UseAuthentication();  // ✅ Must come before UseAuthorization
app.UseAuthorization();

// Global exception handler
app.UseMiddleware<ExceptionHandling>();

// Map controller routes
app.MapControllers();

app.Run();
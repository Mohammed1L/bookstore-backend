using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Security;
using System.Text.Json;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
namespace Middlewares;

public class ExceptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandling> _logger;
    public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
    {
        _next = next;
        _logger =logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {

            _logger.LogError(e, " Exception has occured");
            context.Response.ContentType = "application/json";

            if (e is UnauthorizedAccessException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = e.Message }));
                return;


            }
            else if (e is KeyNotFoundException)
            {

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = e.Message }));
                return;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = e.Message }));
                return;
            }
            
            

        }
        
    }

}
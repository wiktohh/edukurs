using System.Net;
using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infrastructure.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UnauthorazedException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            var response = new { message = ex.Message, stackTrace = ex.StackTrace };
            var payload = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(payload);
        }
        catch (NotFoundException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var response = new { message = ex.Message, stackTrace = ex.StackTrace };
            var payload = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(payload);
        }
        catch (BaseException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new { message = ex.Message, stackTrace = ex.StackTrace };
            var payload = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(payload);
        }
    }
}
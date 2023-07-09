using System.Net;
using System.Text.Json;
using Payments.Protocol.Http.Common;

namespace Payments.WebAPI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception exception)
        {
            this.logger.LogError("An unhandled error occurred while app handle request {Exception}", exception);
            
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var defaultErrorMessage = "Internal service error has occurred. Retry your request later.";
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        
        var result = JsonSerializer.Serialize(
            new BaseResponse(
                false, 
                new List<Error>() {new Error(500, defaultErrorMessage)})
            );
        
        return context.Response.WriteAsync(result);
    }
}
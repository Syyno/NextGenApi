using Payments.Presentation;
using Payments.WebAPI.Extensions;
using Payments.WebAPI.Middleware;

namespace Payments.WebAPI.Definitions;

public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllers()
            .AddApplicationPart(typeof(PresentationAssemblyReference).Assembly)
            .AddNewtonsoftJson()
            .AddModelStateValidation();
        
        builder.Services.AddApiVersioning();
    }
    
    public override void ConfigureApplication(WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.MapControllers();
    }
}
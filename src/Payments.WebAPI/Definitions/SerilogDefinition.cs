using Serilog;

namespace Payments.WebAPI.Definitions;

public class SerilogDefinition: AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        
        builder.Host.UseSerilog(logger);
    }
    
    public override void ConfigureApplication(WebApplication app)
    {
        app.UseSerilogRequestLogging(); 
    }
}
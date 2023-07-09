using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Payments.WebAPI.Configuration;

namespace Payments.WebAPI.Definitions;

public class HealthCheckDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration.Get<AppSettingsConfiguration>()!.Mongo;

        builder.Services
            .AddHealthChecks()
            .AddMongoDb(configuration.ConnectionString);
    }
    
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}


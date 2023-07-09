using Payments.WebAPI.GrpcServices;

namespace Payments.WebAPI.Definitions;

public class GrpcDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddGrpc();
    }
    
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGrpcService<PaymentsService>();
    }
}
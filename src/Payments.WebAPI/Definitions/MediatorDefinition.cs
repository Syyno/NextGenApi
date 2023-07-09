using MediatR;
using Payments.Application;
using Payments.Application.Common.Behavior;

namespace Payments.WebAPI.Definitions;

public class MediatorDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddMediatR(typeof(ApplicationAssemblyReference).Assembly);
        services.AddMediatR(typeof(WebApiAssemblyReference).Assembly);
    }
}
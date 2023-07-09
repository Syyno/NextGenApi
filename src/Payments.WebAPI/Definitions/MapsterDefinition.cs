using Payments.Application;
using Payments.Persistence;
using Payments.Presentation;
using Payments.WebAPI.Extensions;

namespace Payments.WebAPI.Definitions;

public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddMapster(typeof(WebApiAssemblyReference).Assembly);
        builder.Services.AddMapster(typeof(PersistenceAssemblyReference).Assembly);
        builder.Services.AddMapster(typeof(PresentationAssemblyReference).Assembly);
    }
}
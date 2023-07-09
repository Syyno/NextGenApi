using FluentValidation;
using FluentValidation.AspNetCore;

namespace Payments.WebAPI.Definitions;

public class FluentValidationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(WebApiAssemblyReference));
    }
}
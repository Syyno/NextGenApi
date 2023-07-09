using System.Reflection;
using Microsoft.OpenApi.Models;
using Payments.Protocol;
using Payments.Protocol.Http;

namespace Payments.WebAPI.Definitions;

public class SwaggerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payments.WebApi", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlProtocolFile = $"{typeof(ProtocolHttpAssemblyReference).Assembly.GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlProtocolFile));
            c.UseInlineDefinitionsForEnums();
        });
    }

    public override void ConfigureApplication(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentsAPI: pet project"); });
    }
}
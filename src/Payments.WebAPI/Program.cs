using Payments.WebAPI;
using Payments.WebAPI.Configuration;
using Payments.WebAPI.Extensions;
using Serilog;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration.Get<AppSettingsConfiguration>();
    builder.ValidateConfig<AppSettingsConfiguration, AppSettingsConfigurationValidator>(configuration!);
    builder.Services.AddDefinitions(builder, typeof(WebApiAssemblyReference));
    var app = builder.Build();
    app.UseDefinitions();
    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex.Message);
}
finally
{
  Log.CloseAndFlush();
}
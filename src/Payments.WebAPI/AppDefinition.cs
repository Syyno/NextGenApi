namespace Payments.WebAPI;

internal interface IAppDefinition
{
    int OrderIndex { get; }
    bool Enabled { get; }
    void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder);
    void ConfigureApplication(WebApplication app);
}

public abstract class AppDefinition : IAppDefinition
{
    public virtual int OrderIndex => 0;
    public virtual bool Enabled { get; protected set; } = true;
    public virtual void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder) { }
    public virtual void ConfigureApplication(WebApplication app) { }
}

public static class AppDefinitionExtensions
{
    public static void AddDefinitions(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
    {
        var logger = source.BuildServiceProvider().GetRequiredService<ILogger<AppDefinition>>();
        
        var definitions = new List<IAppDefinition>();
        
        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IAppDefinition).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IAppDefinition>().ToList();
            var instancesOrdered = instances.Where(x => x.Enabled).OrderBy(x => x.OrderIndex).ToList();
            
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(@"[AppDefinitions] Founded: {AppDefinitionsCountTotal}. Enabled: {AppDefinitionsCountEnabled}", instances.Count, instancesOrdered.Count);
                logger.LogDebug(@"[AppDefinitions] Registered [{Total}]", string.Join(", ", instancesOrdered.Select(x => x.GetType().Name).ToArray()));
            }

            definitions.AddRange(instancesOrdered);
        }

        definitions.ForEach(app => app.ConfigureServices(source, builder));
        
        source.AddSingleton((IReadOnlyCollection<IAppDefinition>) definitions);
    }

    public static void UseDefinitions(this WebApplication source)
    {
        var definitions = source.Services.GetRequiredService<IReadOnlyCollection<IAppDefinition>>();
        var instancesOrdered = definitions.Where(x => x.Enabled).OrderBy(x => x.OrderIndex).ToList();
        instancesOrdered.ForEach(x => x.ConfigureApplication(source));
    }
}
using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.OpenApi.Models;
using Payments.Protocol;

namespace Payments.WebAPI.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services, Assembly asm)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(asm);
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        
        return services;
    }
}
using FluentValidation;

namespace Payments.WebAPI.Extensions;

/// <summary>
/// Extensions for WebApplicationBuilder
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Config validation method (via fluent validation package)
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceConfiguration"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplicationBuilder ValidateConfig<T, TV>(this WebApplicationBuilder builder, T serviceConfiguration)
        where T : class where TV : AbstractValidator<T>
    {
        var validator = (TV)Activator.CreateInstance(typeof(TV))!;
        var result = validator.Validate(serviceConfiguration);

        if (result.IsValid) 
            return builder;
        
        var errors = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new Exception(string.Join(' ', errors));
    }
}
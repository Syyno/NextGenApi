using FluentValidation;
using Payments.Persistence.Data.Configuration;

namespace Payments.WebAPI.Configuration;

/// <summary>
/// Map from appsettings to this object
/// </summary>
public class AppSettingsConfiguration
{
    public MongoDbConfiguration Mongo { get; set; }
}

/// <summary>
/// Validation for appsettings
/// </summary>
public class AppSettingsConfigurationValidator : AbstractValidator<AppSettingsConfiguration> 
{
    /// <summary>
    /// Rules
    /// </summary>
    public AppSettingsConfigurationValidator()
    {
        RuleFor(r => r.Mongo)
            .NotNull()
            .SetValidator(new MongoDbConfigurationValidator())
            .WithMessage(r => $"Поле {nameof(r.Mongo)} не найдено.");
    }
}
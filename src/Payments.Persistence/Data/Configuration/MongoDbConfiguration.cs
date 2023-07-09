using FluentValidation;

namespace Payments.Persistence.Data.Configuration;

public class MongoDbConfiguration
{
    public string ConnectionString { get; set; }
    public string PaymentsDatabase { get; set; }
    public string PaymentsCollection { get; set; }
}

public class MongoDbConfigurationValidator : AbstractValidator<MongoDbConfiguration>
{
    public MongoDbConfigurationValidator()
    {
        RuleFor(r => r.ConnectionString)
            .NotEmpty()
            .NotNull()
            .WithMessage(r => $"Field {nameof(r.ConnectionString)} cant be empty.");
        
        RuleFor(r => r.PaymentsDatabase)
            .NotEmpty()
            .NotNull()
            .WithMessage(r => $"Field {nameof(r.PaymentsDatabase)} cant be empty.");
        
        RuleFor(r => r.PaymentsCollection)
            .NotEmpty()
            .NotNull()
            .WithMessage(r => $"Field {nameof(r.PaymentsCollection)} cant be empty.");
    }
}
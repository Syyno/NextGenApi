using CSharpFunctionalExtensions;
using Payments.Domain.Entity;
using Payments.Domain.Entity.Payment;

namespace Payments.Domain.ValueObject;

public class ExternalId: SimpleValueObject<string>
{
    private ExternalId(string value) : base(value) { }

    public static Result<ExternalId> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<ExternalId>($"Поле {nameof(Payment.ExternalId)} должно быть заполнено.");    
        return new ExternalId(value);
    }
}
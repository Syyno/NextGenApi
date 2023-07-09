using CSharpFunctionalExtensions;
using Payments.Domain.Entity;
using Payments.Domain.Entity.Payment;

namespace Payments.Domain.ValueObject;

public class PaymentSum: SimpleValueObject<decimal>
{
    private PaymentSum(decimal value) : base(value) { }

    public static Result<PaymentSum> Create(decimal? value)
    {
        if (!value.HasValue)
        {
            return Result.Failure<PaymentSum>($"Поле {nameof(Payment.PaymentSum)} не может быть равным null.");
        }
        
        if (value < 0)
        {
            return Result.Failure<PaymentSum>($"Поле {nameof(Payment.PaymentSum)} не может быть отрицательным.");
        }

        return new PaymentSum(value.Value);
    }
}
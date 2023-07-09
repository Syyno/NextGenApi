using CSharpFunctionalExtensions;
using Payments.Domain.Entity;
using Payments.Domain.Entity.Payment;

namespace Payments.Domain.ValueObject;

public class PaymentType : SimpleValueObject<PaymentTypeEnum>
{
    private PaymentType(PaymentTypeEnum value) : base(value)
    {
    }

    public static Result<PaymentType> Create(int value)
    {
        if (!Enum.IsDefined(typeof(PaymentTypeEnum), value))
        {
            return Result.Failure<PaymentType>($"Поле {nameof(Payment.PaymentType)} имеет невалидное значение.");
        }

        return new PaymentType((PaymentTypeEnum)value);
    }
}

public enum PaymentTypeEnum
{
    /// <summary>
    /// Наличные
    /// </summary>
    Cash = 1,
    
    /// <summary>
    /// Карта
    /// </summary>
    Card
}
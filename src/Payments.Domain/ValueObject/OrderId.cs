using CSharpFunctionalExtensions;
using Payments.Domain.Entity;
using Payments.Domain.Entity.Payment;

namespace Payments.Domain.ValueObject;

public class OrderId : SimpleValueObject<string>
{
    private OrderId(string value) : base(value) { }

    public static Result<OrderId> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<OrderId>($"Поле {nameof(Payment.OrderId)} должно быть заполнено.");    
        return new OrderId(value);
    }
}
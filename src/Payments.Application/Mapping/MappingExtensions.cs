using CSharpFunctionalExtensions;
using Payments.Application.UseCases.Payment.Command.Create;
using Payments.Domain.ValueObject;
using DomainPayment = Payments.Domain.Entity.Payment.Payment;

namespace Payments.Application.Mapping;

public static class MappingExtensions
{
    public static Result<DomainPayment> MapToDomain(this Create createPaymentCommand)
    {
        var payment = createPaymentCommand.Payment;
        var orderId = OrderId.Create(payment.OrderId);
        var externalId = ExternalId.Create(payment.ExternalId);
        var paymentSum = PaymentSum.Create(payment.PaymentSum);
        var paymentType = PaymentType.Create((int) payment.PaymentType);

        return DomainPayment.InstantiateInitial(orderId, externalId, paymentSum, paymentType);
    }
}
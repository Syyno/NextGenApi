using CSharpFunctionalExtensions;
using Payments.Domain.ValueObject;
using Payments.Persistence.Data.Models;
using DomainPayment = Payments.Domain.Entity.Payment.Payment;

namespace Payments.Persistence.Mapping;

public static class MappingExtensions
{
    public static Result<DomainPayment> MapToDomain(this Payment payment)
    {
        var id = payment.Id;
        var orderId = OrderId.Create(payment.OrderId);
        var externalId = ExternalId.Create(payment.ExternalId);
        var paymentSum = PaymentSum.Create(payment.PaymentSum);
        var paymentType = PaymentType.Create((int) payment.PaymentType);

        return DomainPayment.Instantiate(id, orderId, externalId, payment.CreatedAt, paymentSum, paymentType);
    }
}
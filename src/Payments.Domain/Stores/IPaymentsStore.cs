using CSharpFunctionalExtensions;
using Payments.Domain.Filters;
using DomainPayment = Payments.Domain.Entity.Payment.Payment;

namespace Payments.Domain.Stores;

public interface IPaymentsStore
{
    Task<string> CreateAsync(DomainPayment payment, CancellationToken token = default);
    Task<Maybe<Result<DomainPayment>>> ReadAsync(PaymentFilter filter, CancellationToken token = default);
    Task UpdateAsync(PaymentFilter filter, DomainPayment payment, CancellationToken token = default);
    Task<bool> DeleteAsync(PaymentFilter filter, CancellationToken token = default);
}
using MediatR;
using Payments.Application.Common;
using Payments.Domain.Entity.Payment;
using Payments.Domain.Filters;
using Payments.Domain.Stores;


namespace Payments.Application.UseCases.Payment.Query.Read;

public class ReadQueryHandler : IRequestHandler<Read, HandlingResult>
{
    private readonly IPaymentsStore _paymentsStore;

    public ReadQueryHandler(IPaymentsStore paymentsStore)
    {
        _paymentsStore = paymentsStore;
    }
    
    public async Task<HandlingResult> Handle(Read request, CancellationToken cancellationToken)
    {
        var payment = await _paymentsStore.ReadAsync(
            PaymentFilter.Create(request.PaymentId, request.PaymentExternalId), 
            cancellationToken);
        
        if (payment.HasNoValue)
            return HandlingResult.NotFound(PaymentErrorMessages.NotFound($"{request.PaymentId}/{request.PaymentExternalId}"));
        
        if (payment.Value.IsFailure)
            return HandlingResult.BadRequest(payment.Value.Error);

        return HandlingResult.Ok(payment.Value.Value);
    }
}
using MediatR;
using Payments.Application.Common;
using Payments.Domain.Entity.Payment;
using Payments.Domain.Filters;
using Payments.Domain.Stores;

namespace Payments.Application.UseCases.Payment.Command.Delete;

public class DeleteCommandHandler : IRequestHandler<Delete, HandlingResult>
{
    private readonly IPaymentsStore _paymentsStore;

    public DeleteCommandHandler(IPaymentsStore paymentsStore)
    {
        _paymentsStore = paymentsStore;
    }
    
    public async Task<HandlingResult> Handle(Delete request, CancellationToken cancellationToken)
    {
        var filter = PaymentFilter.Create(request.PaymentId, request.PaymentExternalId);
        var payment = await _paymentsStore.ReadAsync(
            filter, 
            cancellationToken);
        
        if (payment.HasNoValue)
            return HandlingResult.NotFound(PaymentErrorMessages.NotFound($"{request.PaymentId}/{request.PaymentExternalId}"));

        await _paymentsStore.DeleteAsync(filter, cancellationToken);
        
        return HandlingResult.Ok(payment.Value.Value);
    }
}
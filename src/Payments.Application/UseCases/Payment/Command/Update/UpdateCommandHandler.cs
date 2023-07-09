using Mapster;
using MediatR;
using Payments.Application.Common;
using Payments.Application.Mapping;
using Payments.Domain.DTO;
using Payments.Domain.Entity.Payment;
using Payments.Domain.Filters;
using Payments.Domain.Stores;

namespace Payments.Application.UseCases.Payment.Command.Update;

public class UpdateCommandHandler : IRequestHandler<Update, HandlingResult>
{
    private readonly IPaymentsStore _paymentsStore;

    public UpdateCommandHandler(IPaymentsStore paymentsStore)
    {
        _paymentsStore = paymentsStore;
    }

    public async Task<HandlingResult> Handle(Update request, CancellationToken cancellationToken)
    {
        var filter = PaymentFilter.Create(request.Payment.Id, request.Payment.ExternalId);
        var payment = await _paymentsStore.ReadAsync(filter, cancellationToken);

        if (payment.HasNoValue)
            return HandlingResult.NotFound(PaymentErrorMessages.NotFound($"{request.Payment.Id}/{request.Payment.ExternalId}"));

        var updateDto = request.Payment.Adapt<UpdatePaymentDto>();

        var domainPayment = payment.Value;

        var updateResult = domainPayment.Value.Update(updateDto);

        if (updateResult.IsFailure)
            return HandlingResult.BadRequest(updateResult.Error);
        
        await _paymentsStore.UpdateAsync(filter, domainPayment.Value, cancellationToken);
        
        return HandlingResult.Ok(domainPayment.Value);
    }
}
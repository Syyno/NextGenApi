using Mapster;
using MediatR;
using Payments.Application.Common;
using Payments.Application.Mapping;
using Payments.Domain.Entity.Payment;
using Payments.Domain.Filters;
using Payments.Domain.Stores;
namespace Payments.Application.UseCases.Payment.Command.Create;

public class CreateCommandHandler : IRequestHandler<Create, HandlingResult>
{
    private readonly IPaymentsStore _paymentsStore;

    public CreateCommandHandler(IPaymentsStore paymentsStore)
    {
        _paymentsStore = paymentsStore;
    }
    

    public async Task<HandlingResult> Handle(Create command, CancellationToken cancellationToken)
    {
        var filter = PaymentFilter.ByExternalId(command.Payment.ExternalId);
        var payment = await _paymentsStore.ReadAsync(filter, cancellationToken);

        if (payment.HasValue)
            return HandlingResult.BadRequest(PaymentErrorMessages.AlreadyExists(command.Payment.ExternalId));
        
        var domainModel = command.MapToDomain();
        
        if (domainModel.IsFailure)
            return HandlingResult.BadRequest(domainModel.Error);
        
        var id = await this._paymentsStore.CreateAsync(domainModel.Value, cancellationToken);
        domainModel.Value.SetId(id);
        //var model = domainModel.Value.Adapt<ProtocolPayment>();
        return HandlingResult.Ok(domainModel.Value);
    }
}
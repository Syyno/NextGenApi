using MediatR;
using Payments.Application.Common;

namespace Payments.Application.UseCases.Payment.Command.Delete;

public class Delete : IRequest<HandlingResult>
{
    public string PaymentId { get; set; }
    public string PaymentExternalId { get; set; }
}
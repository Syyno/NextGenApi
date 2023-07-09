using MediatR;
using Payments.Application.Common;

namespace Payments.Application.UseCases.Payment.Command.Update;

public class Update : IRequest<HandlingResult>
{
    public PaymentUpdateModel Payment { get; set; }
}
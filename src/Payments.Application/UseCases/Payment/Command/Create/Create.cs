using MediatR;
using Payments.Application.Common;
using Payments.Application.UseCases.Payment.Models;

namespace Payments.Application.UseCases.Payment.Command.Create;

public class Create: IRequest<HandlingResult>
{
    public PaymentiInitial Payment { get; set; }
}


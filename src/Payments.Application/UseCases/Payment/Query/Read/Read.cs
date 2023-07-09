using MediatR;
using Payments.Application.Common;

namespace Payments.Application.UseCases.Payment.Query.Read;

public class Read : IRequest<HandlingResult>
{
    public Read(string paymentId, string paymentExternalId)
    {
        PaymentId = paymentId;
        PaymentExternalId = paymentExternalId;
    }
    
    public string PaymentId { get; set; }
    public string PaymentExternalId { get; set; }
}

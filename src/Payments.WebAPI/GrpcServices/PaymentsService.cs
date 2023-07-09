using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Payments.Application.UseCases.Payment.Query.Read;
using PaymentsAPI.Protocol;

namespace Payments.WebAPI.GrpcServices;

/// <summary>
/// Service for GRPC
/// </summary>
public class PaymentsService : PaymentsRpc.PaymentsRpcBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Ctor for DI
    /// </summary>
    /// <param name="mediator"></param>
    public PaymentsService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<PaymentResponse> GetPayment(PaymentRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(
            new Read(request.Id, request.ExternalId),
            context.CancellationToken);


        if (result.IsFailure)
        {
            return new PaymentResponse()
            {
                IsSuccess = false,
                Errors =
                {
                    new PaymentsAPI.Protocol.Error()
                    {
                        Message = result.Errors.First().Message,
                        ErrorCode = result.Errors.First().ErrorCode
                    }
                }
            };
            
        }

        var rd = result.Data as Payments.Protocol.Http.Payments.Payment;

        return new PaymentResponse()
        {
            IsSuccess = true,
            Data = new Payment()
            {
                Id = rd.Id,
                ExternalId = rd.ExternalId,
                CreatedAt = rd.CreatedAt.ToTimestamp(),
                OrderId = rd.OrderId,
                PaymentSum = (double)rd.PaymentSum,
                PaymentType = (int)rd.PaymentType
            }
        };
    }
}

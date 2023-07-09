using System.Text.Json.Serialization;
using Payments.Application.UseCases.Payment.Command.Update;
using Payments.Protocol.Http.Payments;

namespace Payments.Presentation.Models;

public class UpdatePaymentInternalRequest
{
    /// <summary>
    /// Сумма платежа в рублях
    /// </summary>
    public decimal PaymentSum { get; init; }
    [JsonIgnore]
    public bool PaymentSumSpecified { get; set; }

    /// <summary>
    /// Вид платежа
    /// </summary>
    public int PaymentType { get; init; }
    
    /// <summary>
    /// Свойство для сериалайзера (что бы понять было ли это поле в запросе)
    /// </summary>
    [JsonIgnore]
    public bool PaymentTypeSpecified { get; set; }
}

/// <summary>
/// Mappings
/// </summary>
public static class UpdatePaymentInternalRequestMappingExtension
{
    /// <summary>
    /// Map from presentational layer model to application layer model
    /// </summary>
    /// <param name="request"></param>
    /// <param name="idRequest"></param>
    /// <returns></returns>
    public static Update MapToCommand(this UpdatePaymentInternalRequest request, PaymentIdRequest idRequest)
    {
        return new Update()
        {
            Payment = new PaymentUpdateModel()
            {
                Id = idRequest.PaymentId,
                ExternalId = idRequest.PaymentExternalId,
                PaymentSum = request.PaymentSum,
                PaymentSumSpecified = request.PaymentSumSpecified,
                PaymentType = request.PaymentType,
                PaymentTypeSpecified = request.PaymentTypeSpecified
            }
        };
    }
}
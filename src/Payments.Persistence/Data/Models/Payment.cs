using Mapster;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Payments.Persistence.Data.Models;

public class Payment
{
    /// <summary>
    /// id документа в монго
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Внешний id
    /// </summary>
    public string ExternalId { get; set; }
    
    /// <summary>
    /// Id отправления 
    /// </summary>
    public string OrderId { get; init; }

    /// <summary>
    /// Время создания платежа
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Сумма платежа в рублях
    /// </summary>
    public decimal PaymentSum { get; init; }
    
    /// <summary>
    /// Вид платежа
    /// </summary>
    public int PaymentType { get; init; }
}

public class PaymentMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Domain.Entity.Payment.Payment, Payment>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ExternalId, src => src.ExternalId.Value)
            .Map(dest => dest.OrderId, src => src.OrderId.Value)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.PaymentSum, src => src.PaymentSum.Value)
            .Map(dest => dest.PaymentType, src => src.PaymentType.Value);

    }
}

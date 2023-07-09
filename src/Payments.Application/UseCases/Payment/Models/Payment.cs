namespace Payments.Application.UseCases.Payment.Models;

public class Payment
{
    /// <summary>
    /// id документа в монго
    /// </summary>
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
    public PaymentTypeEnum PaymentType { get; init; }
}
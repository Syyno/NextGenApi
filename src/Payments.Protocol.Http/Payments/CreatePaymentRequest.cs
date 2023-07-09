namespace Payments.Protocol.Http.Payments;

public class CreatePaymentRequest
{
    /// <summary>
    /// Id заказа 
    /// </summary>
    public string OrderId { get; set; }
    
    /// <summary>
    /// Внешний id
    /// </summary>
    public string ExternalId { get; set; }
    
    /// <summary>
    /// Сумма платежа
    /// </summary>
    public decimal PaymentSum { get; set; }

    /// <summary>
    /// Вид платежа
    /// </summary>
    public PaymentTypeEnum PaymentType { get; set; }
}
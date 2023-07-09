namespace Payments.Application.UseCases.Payment.Models;

public class PaymentiInitial
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

    public PaymentTypeEnum PaymentType { get; set; }
}

public enum PaymentTypeEnum
{
    /// <summary>
    /// Наличные
    /// </summary>
    Cash = 1,
    
    /// <summary>
    /// Карта
    /// </summary>
    Card
}
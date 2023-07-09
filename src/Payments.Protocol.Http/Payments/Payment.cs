﻿namespace Payments.Protocol.Http.Payments;

public class Payment
{
    /// <summary>
    /// Внутренний id
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
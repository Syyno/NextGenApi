namespace Payments.Application.UseCases.Payment.Command.Update;

public class PaymentUpdateModel
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
    /// Сумма платежа в рублях
    /// </summary>
    public decimal PaymentSum { get; init; }
    
    /// <summary>
    /// Вид платежа
    /// </summary>
    public int PaymentType { get; init; }
    
    public bool PaymentSumSpecified { get; set; }
    public bool PaymentTypeSpecified { get; set; }
}
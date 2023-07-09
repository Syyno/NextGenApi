namespace Payments.Protocol.Http.Payments;

public class UpdatePaymentRequest
{
    /// <summary>
    /// Сумма платежа в рублях
    /// </summary>
    public decimal PaymentSum { get; init; }
    
    /// <summary>
    /// Вид платежа
    /// </summary>
    public int PaymentType { get; init; }
}
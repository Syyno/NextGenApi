namespace Payments.Domain.DTO;

public class UpdatePaymentDto
{
    /// <summary>
    /// Сумма платежа в рублях
    /// </summary>
    public decimal PaymentSum { get; init; }
    public bool PaymentSumSpecified { get; set; }

    /// <summary>
    /// Вид платежа
    /// </summary>
    public int PaymentType { get; init; }
    public bool PaymentTypeSpecified { get; set; }
}
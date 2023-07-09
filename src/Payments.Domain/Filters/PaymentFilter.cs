namespace Payments.Domain.Filters;

public class PaymentFilter
{
    public string PaymentId { get; set; }
    public string PaymentExternalId { get; set; }
    
    private PaymentFilter (string paymentId, string paymentExternalId)
    {
        PaymentId = paymentId;
        PaymentExternalId = paymentExternalId;
    }

    public static PaymentFilter Create(string paymentId, string externalId)
    {
        return new(paymentId, externalId);
    }
    
    public static PaymentFilter ByPaymentId(string paymentId)
    {
        return new PaymentFilter(
            paymentId: paymentId,
            paymentExternalId: null!
        );
    }
    
    public static PaymentFilter ByExternalId(string paymentExternalId)
    {
        return new PaymentFilter(
            paymentId: null!,
            paymentExternalId: paymentExternalId
        );
    }
}
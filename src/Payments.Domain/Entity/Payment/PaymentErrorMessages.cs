namespace Payments.Domain.Entity.Payment;

public static class PaymentErrorMessages
{
    public static string NotFound(string id) => $"Payment with id {id} not found.";
    public static string AlreadyExists(string id) => $"Payment with id {id} already exists.";
}
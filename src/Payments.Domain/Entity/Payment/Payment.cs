using CSharpFunctionalExtensions;
using Payments.Domain.DTO;
using Payments.Domain.ValueObject;

namespace Payments.Domain.Entity.Payment;

public class Payment
{
    #region props
    
    /// <summary>
    /// id документа в монго
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// Внешний id
    /// </summary>
    public ExternalId ExternalId{ get; private set; }
    
    /// <summary>
    /// Id заказа 
    /// </summary>
    public OrderId OrderId { get; private set; }

    /// <summary>
    /// Время создания платежа
    /// </summary>
    public DateTime CreatedAt { get; init;}

    /// <summary>
    /// Сумма платежа в рублях
    /// </summary>
    public PaymentSum PaymentSum { get; private set; }
    
    /// <summary>
    /// Вид платежа
    /// </summary>
    public PaymentType PaymentType { get; private set; }
    
    #endregion
    
    #region setters

    public void SetId(string id)
    {
        this.Id = id;
    }

    public void SetPaymentSum(PaymentSum paymentSum)
    {
        this.PaymentSum = paymentSum;
    }

    public void SetPaymentType(PaymentType paymentType)
    {
        this.PaymentType = paymentType;
    }
    
    public void SetOrderId(OrderId orderId)
    {
        this.OrderId = orderId;
    }
    
    public void SetExternalId(ExternalId externalId)
    {
        this.ExternalId = externalId;
    }
    
    #endregion

    #region ctors

    private Payment( 
        OrderId orderId, 
        ExternalId externalId,
        DateTime createdAt,
        PaymentSum paymentSum,
        PaymentType paymentType
       )
    {
        OrderId = orderId;
        ExternalId = externalId;
        CreatedAt = createdAt;
        PaymentSum = paymentSum;
        PaymentType = paymentType;
    }

    private Payment(string id, 
        OrderId orderId,
        ExternalId externalId,
        DateTime createdAt,
        PaymentSum paymentSum, 
        PaymentType paymentType
        )
    {
        Id = id;
        OrderId = orderId;
        ExternalId = externalId;
        CreatedAt = createdAt;
        PaymentSum = paymentSum;
        PaymentType = paymentType;
    }
    
    #endregion

    #region methods

     /// <summary>
    /// Вызывается при первом создании экземпляра
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="paymentSum"></param>
    /// <param name="agentFee"></param>
    /// <returns></returns>
    public static Result<Payment> InstantiateInitial(
         Result<OrderId> orderId, 
         Result<ExternalId> externalId,
         Result<PaymentSum> paymentSum,
         Result<PaymentType> paymentType
       )
    {
        if (orderId.IsFailure)
            return Result.Failure<Payment>(orderId.Error);
        
        if (externalId.IsFailure)
            return Result.Failure<Payment>(externalId.Error);

        if (paymentSum.IsFailure)
            return Result.Failure<Payment>(paymentSum.Error);
        
        if (paymentType.IsFailure)
            return Result.Failure<Payment>(paymentType.Error);
        
        return new Payment(orderId.Value, 
            externalId.Value,
            DateTime.UtcNow, //todo - fix, bad practice. Better get this from request body directly
            paymentSum.Value, 
            paymentType.Value);
    }

    /// <summary>
    /// Вызывается при создании экземпляра по данным из data layer
    /// </summary>
    public static Result<Payment> Instantiate(string id,
        Result<OrderId> orderId,
        Result<ExternalId> externalId,
        DateTime createdAt, 
        Result<PaymentSum> paymentSum,
        Result<PaymentType> paymentType)
    {
        if (orderId.IsFailure)
            return Result.Failure<Payment>(orderId.Error);
        
        if (externalId.IsFailure)
            return Result.Failure<Payment>(externalId.Error);

        if (paymentSum.IsFailure)
            return Result.Failure<Payment>(paymentSum.Error);
        
        if (paymentType.IsFailure)
            return Result.Failure<Payment>(paymentType.Error);

        return new Payment(id,
            orderId.Value,
            externalId.Value,
            createdAt, 
            paymentSum.Value, 
           paymentType.Value);
    }
    
    public Result Update(UpdatePaymentDto current)
    {
        if (current.PaymentSumSpecified)
        {
            var paymentSum = PaymentSum.Create(current.PaymentSum);
            if (paymentSum.IsFailure)
                return Result.Failure(paymentSum.Error);
            this.SetPaymentSum(paymentSum.Value);
        }
    
        if (current.PaymentTypeSpecified)
        {
            var paymentType = PaymentType.Create((int)current.PaymentType);
            if (paymentType.IsFailure)
                return Result.Failure(paymentType.Error);
            this.SetPaymentType(paymentType.Value);
        }
    
        return Result.Success();
    }
    
    #endregion
}
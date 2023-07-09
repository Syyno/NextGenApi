using System.Linq.Expressions;
using SpeciVacation;

namespace Payments.Persistence.Data.Specifications.Payment;

public class IdSpecification: Specification<Models.Payment>
{
    private readonly string paymentId;
    
    public IdSpecification(string value)
    {
        this.paymentId = value;
    }
    
    public override Expression<Func<Models.Payment, bool>> ToExpression()
    {
        return payment => payment.Id.ToString() == this.paymentId;
    }
}
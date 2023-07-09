using System.Linq.Expressions;
using SpeciVacation;

namespace Payments.Persistence.Data.Specifications.Payment;

public class ExternalIdSpecification: Specification<Models.Payment>
{
    private readonly string externalId;
    
    public ExternalIdSpecification(string value) {
        this.externalId = value;
    }
    
    public override Expression<Func<Models.Payment, bool>> ToExpression()
    {
        return payment => payment.ExternalId == this.externalId;
    }
}
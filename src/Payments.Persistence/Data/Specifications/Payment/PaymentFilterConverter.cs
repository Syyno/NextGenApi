using System.Linq.Expressions;
using Payments.Domain.Filters;
using SpeciVacation;

namespace Payments.Persistence.Data.Specifications.Payment;

public static class PaymentFilterConverter
{
    public static Expression<Func<Models.Payment, bool>> ToExpression(this PaymentFilter filter)
    {
        var spec = Specification<Models.Payment>.All;
        
        if (!string.IsNullOrWhiteSpace(filter.PaymentId))
        {
            spec = spec.And(new IdSpecification(filter.PaymentId));
            return spec.ToExpression();
        }
        
        if (!string.IsNullOrWhiteSpace(filter.PaymentExternalId))
        {
            spec = spec.And(new ExternalIdSpecification(filter.PaymentExternalId));
            return spec.ToExpression();
        }

        throw new ArgumentException("Cant find specification to build filter");
    }
}
using CSharpFunctionalExtensions;
using Mapster;
using MongoDB.Driver;
using Payments.Domain.Filters;
using Payments.Domain.Stores;
using Payments.Persistence.Data.Specifications.Payment;
using Payments.Persistence.Data.Models;
using Payments.Persistence.Mapping;
using DomainPayment = Payments.Domain.Entity.Payment.Payment;

namespace Payments.Persistence.Data.Stores.Payments;

public class PaymentsMongoStore : IPaymentsStore
{
    private readonly IMongoCollection<Payment> _paymentsCollection;

    public PaymentsMongoStore(IMongoCollection<Payment> paymentsCollection)
    {
        _paymentsCollection = paymentsCollection;
    }
    
    public async Task<string> CreateAsync(DomainPayment payment, CancellationToken token = default)
    {
        var dataModel = payment.Adapt<Models.Payment>();
        await this._paymentsCollection.InsertOneAsync(dataModel, null, token);
        return dataModel.Id;
    }

    public async Task<Maybe<Result<DomainPayment>>> ReadAsync(PaymentFilter filter, CancellationToken token = default)
    {
        var payment = await this._paymentsCollection.Find(filter.ToExpression()).FirstOrDefaultAsync(token);
        if (payment is null)
            return Maybe<Result<DomainPayment>>.None;
        var domainModel = payment.MapToDomain();
        return domainModel.IsSuccess 
            ? Maybe<Result<DomainPayment>>.From(domainModel.Value) 
            : Maybe.From(Result.Failure<DomainPayment>(domainModel.Error));
    }

    public async Task UpdateAsync(PaymentFilter filter, Domain.Entity.Payment.Payment payment, CancellationToken token = default)
    {
        var options = new FindOneAndUpdateOptions<Payment>()
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = false
        };

        var updateDefinition = Builders<Models.Payment>.Update
            .Set(x => x.PaymentSum, payment.PaymentSum.Value)
            .Set(x => x.PaymentType, (int)payment.PaymentType.Value);
        
        await this._paymentsCollection.FindOneAndUpdateAsync(filter.ToExpression(), updateDefinition, options, token);
    }

    public async Task<bool> DeleteAsync(PaymentFilter filter, CancellationToken token = default)
    {
        var res = await _paymentsCollection.DeleteOneAsync(filter.ToExpression(), token);
        return res.DeletedCount == 1;
    }
}
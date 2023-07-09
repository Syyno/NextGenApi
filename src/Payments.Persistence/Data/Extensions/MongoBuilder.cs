using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Payments.Persistence.Data.Configuration;
using Payments.Persistence.Data.Models;

namespace Payments.Persistence.Data.Extensions;

public class MongoBuilder
{
    private readonly MongoDbConfiguration _config;
    private readonly IServiceCollection _services;

    public MongoBuilder(
        MongoDbConfiguration config,
        IServiceCollection services
    )
    {
        _config = config;
        _services = services;
    }

    public MongoBuilder AddPaymentsDatabase()
    {
        _services.AddSingleton(serviceCollection =>
        {
            var mongoClient = serviceCollection.GetService<MongoClient>();
            return mongoClient!.GetDatabase(_config.PaymentsDatabase);
        });

        return this;
    }

    public MongoBuilder WithPaymentsCollection()
    {
        _services.AddSingleton(serviceCollection =>
        {
            var mongoDatabase = serviceCollection.GetService<IMongoDatabase>();
            var collection = mongoDatabase!.GetCollection<Models.Payment>(_config.PaymentsCollection);
            var options = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<Models.Payment>(nameof(Models.Payment.ExternalId));
            var indexDefinition = new IndexKeysDefinitionBuilder<Models.Payment>().Ascending(field);
            
            mongoDatabase!
                .GetCollection<Models.Payment>(_config.PaymentsCollection).
                Indexes.CreateOne( new CreateIndexModel<Payment>(indexDefinition, options));
           
            return collection;
        });

        return this;
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Payments.Domain.Stores;
using Payments.Persistence.Data.Extensions;
using Payments.Persistence.Data.Stores.Payments;
using Payments.WebAPI.Configuration;

namespace Payments.WebAPI.Definitions;

public class PersistenceDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration.Get<AppSettingsConfiguration>()!.Mongo;
        
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
        BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
      
        services
            .AddMongo(configuration)
            .AddPaymentsDatabase()
            .WithPaymentsCollection();

        services.AddScoped<IPaymentsStore, PaymentsMongoStore>();
    }
}
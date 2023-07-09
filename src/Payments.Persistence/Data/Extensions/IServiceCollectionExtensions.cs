using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using Payments.Persistence.Data.Configuration;

namespace Payments.Persistence.Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static MongoBuilder AddMongo(this IServiceCollection services, MongoDbConfiguration mongoDbConfiguration)
    {
        var client = new MongoClient(mongoDbConfiguration.ConnectionString);

        try 
        {
            client.ListDatabaseNames();
        }
        catch (Exception e)
        {
            throw new Exception($"Mongo connection attempt failed. Exception: {e.Message}");
        }

        if(client.Cluster.Description.State != ClusterState.Connected)
            throw new Exception($"Mongo connection attempt failed. Cluster state is {client.Cluster.Description.State}");
        
        services.AddSingleton(_ => new MongoClient(mongoDbConfiguration.ConnectionString));
        return new MongoBuilder(mongoDbConfiguration, services);
    }
}

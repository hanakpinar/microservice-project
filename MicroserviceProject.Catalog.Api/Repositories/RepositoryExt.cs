using MicroserviceProject.Catalog.Api.Options;
using MongoDB.Driver;

namespace MicroserviceProject.Catalog.Api.Repositories;

public static class RepositoryExt
{
    public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var options = sp.GetRequiredService<MongoOption>();
            return new MongoClient(options.ConnectionString);
        });

        services.AddScoped<AppDbContext>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var options = sp.GetRequiredService<MongoOption>();

            return AppDbContext.Create(client.GetDatabase(options.DatabaseName));
        });

        return services;
    }
}
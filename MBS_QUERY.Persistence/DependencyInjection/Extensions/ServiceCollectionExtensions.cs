using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Persistence.DependencyInjection.Options;
using MBS_QUERY.Persistence.Repositories;

namespace MBS_QUERY.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(_ =>
            configuration.GetSection(nameof(MongoDbSettings)));

        services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }
}
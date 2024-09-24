using MBS_QUERY.Application.Abstractions;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MBS_QUERY.Infrastructure.Caching;
using MBS_QUERY.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace MBS_QUERY.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServicesInfrastructure(this IServiceCollection services)
        => services.AddTransient<ICacheService, CacheService>();
    
    // Configure Redis
    public static void AddRedisInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            var connectionString = configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connectionString;
        });
    }
}
using System.Reflection;
using MBS_QUERY.Application.Abstractions;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MBS_QUERY.Infrastructure.Caching;
using MBS_QUERY.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using MassTransit;
using MBS_QUERY.Contract.JsonConverters;
using MBS_QUERY.Infrastructure.PipeObservers;
using Newtonsoft.Json;

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
    
    // Configure for masstransit with rabbitMQ
    public static IServiceCollection AddMasstransitRabbitMQInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
        
        services.AddMassTransit(cfg =>
        {
            // ===================== Setup for Consumer =====================
            cfg.AddConsumers(Assembly.GetExecutingAssembly()); // Add all of consumers to masstransit instead above command
    
            // ?? => Configure endpoint formatter. Not configure for producer Root Exchange
            cfg.SetKebabCaseEndpointNameFormatter(); // ??
    
            cfg.UsingRabbitMq((context, bus) =>
            {
                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.Port, masstransitConfiguration.VHost, h =>
                {
                    h.Username(masstransitConfiguration.UserName);
                    h.Password(masstransitConfiguration.Password);
                });
    
                bus.UseNewtonsoftJsonSerializer();
    
                bus.ConfigureNewtonsoftJsonSerializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());
                    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                    return settings;
                });
    
                bus.ConfigureNewtonsoftJsonDeserializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());
                    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                    return settings;
                });
    
                bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConnectPublishObserver(new LoggingPublishObserver());
                bus.ConnectSendObserver(new LoggingSendObserver());
    
                // Rename for Root Exchange and setup for consumer also
                bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
    
                // ===================== Setup for Consumer =====================
    
                // Importantce to create Echange and Queue
                bus.ConfigureEndpoints(context);
            });
        });
    
        return services;
    }
}
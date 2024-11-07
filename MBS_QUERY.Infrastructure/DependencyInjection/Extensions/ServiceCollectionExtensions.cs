using System.Reflection;
using HealthChecks.UI.Client;
using MassTransit;
using MBS_QUERY.Application.Abstractions;
using MBS_QUERY.Contract.JsonConverters;
using MBS_QUERY.Infrastructure.Caching;
using MBS_QUERY.Infrastructure.DependencyInjection.Options;
using MBS_QUERY.Infrastructure.PipeObservers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MBS_QUERY.Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddServicesInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICacheService, CacheService>();
    }

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
    public static IServiceCollection AddMasstransitRabbitMqInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

        var messageBusOption = new MessageBusOptions();
        configuration.GetSection(nameof(MessageBusOptions)).Bind(messageBusOption);

        services.AddMassTransit(cfg =>
        {
            // ===================== Setup for Consumer =====================
            cfg.AddConsumers(Assembly
                .GetExecutingAssembly()); // Add all of consumers to masstransit instead above command

            // ?? => Configure endpoint formatter. Not configure for producer Root Exchange
            cfg.SetKebabCaseEndpointNameFormatter(); // ??

            cfg.UsingRabbitMq((context, bus) =>
            {
                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.Port, masstransitConfiguration.VHost,
                    h =>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });

                bus.UseMessageRetry(retry
                    => retry.Incremental(
                        messageBusOption.RetryLimit,
                        messageBusOption.InitialInterval,
                        messageBusOption.IntervalIncrement));

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

    public static void AddMediatRInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
    }

    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services,
        IConfiguration Configuration)
    {
        var hc = services.AddHealthChecks();
        hc.AddMongoDb(
            mongodbConnectionString: Configuration.GetSection("MongoDbSettings").GetValue<string>("ConnectionString"),
            name: "MongoDbConnection",
            tags: ["database", "MongoDb"]
        );
        services.AddHealthChecksUI(setup =>
        {
            setup.AddHealthCheckEndpoint("MBS_QUERY", "/hc");
            setup.SetEvaluationTimeInSeconds(60); // Configures the UI to poll for updates every 60 seconds
        }).AddInMemoryStorage(); // Adds in-memory storage for the UI data
        return services;
    }
    public static IEndpointRouteBuilder MapDefaultHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return endpoints;
    }

    public static IEndpointRouteBuilder MapDefaultHealthChecksUI(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecksUI(settings => settings.UIPath = "/hc-ui");
        return endpoints;
    }
}
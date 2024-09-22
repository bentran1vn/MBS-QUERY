using FluentValidation;
using MBS_COMMAND.Application.Behaviors;
using MBS_QUERY.Application.Behaviors;
using MBS_QUERY.Application.Mapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MBS_QUERY.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRApplication(this IServiceCollection services)
        => services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly)
            )
            // .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>),typeof(CachingPipelineBehaviorCachingBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
            .AddValidatorsFromAssembly(MBS_QUERY.Contract.AssemblyReference.Assembly, includeInternalTypes: true);

    public static IServiceCollection AddAutoMapperApplication(this IServiceCollection services)
        => services.AddAutoMapper(typeof(ServiceProfile));
}
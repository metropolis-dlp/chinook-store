using System.Reflection;
using ChinookStore.Application._Common.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace ChinookStore.Application;

public static class ServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
          config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
        });

        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
          config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

          /*
          config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
          config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
          config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
          config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
          */
        });
    }
}

using System.Text.Json.Serialization;
using ChinookStore.Web.Infrastructure;

namespace ChinookStore.Web;

public static class ServicesConfiguration
{
    public static void AddWebServices(this IServiceCollection services)
    {
        // - Controllers
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        
        // - Exceptions
        services.AddExceptionHandler<CustomExceptionHandler>();

        // - OpenAPI
        services.AddOpenApiDocument(config =>
        {
            config.Title = "Chinook Store API";
        });
    }
}
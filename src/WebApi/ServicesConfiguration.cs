using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChinookStore.Web.Common;

namespace ChinookStore.Web;

public static class ServicesConfiguration
{
    public static void AddWebServices(this IServiceCollection services)
    {
        // - Controllers
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new DateOnlyAlwaysJsonConverter());
            });

        // - Exceptions
        services.AddExceptionHandler<CustomExceptionHandler>();

        // - OpenAPI
        services.AddOpenApiDocument(config =>
        {
            config.Title = "Chinook Store API";
        });
    }
}

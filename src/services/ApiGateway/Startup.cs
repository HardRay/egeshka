using Egeshka.ApiGateway.Extensions;
using System.Text.Json.Serialization;

namespace Egeshka.ApiGateway;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddApplicationServices()
            .ConfigureAuthentication()
            .ConfigureGrpc(configuration);

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase));
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddExceptionHandlers();
    }

    public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
    {
        applicationBuilder.UseExceptionHandler(_ => { });

        applicationBuilder.UseRouting();
        applicationBuilder.UseAuthentication();
        applicationBuilder.UseAuthorization();

        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();

        applicationBuilder.UseEndpoints(
            endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
    }
}

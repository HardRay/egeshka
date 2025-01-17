using Egeshka.ApiGateway.Providers;
using Egeshka.ApiGateway.Providers.Interfaces;
using Egeshka.Auth.Grpc;
using System.Text.Json.Serialization;

namespace Egeshka.ApiGateway;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        AddServices(serviceCollection);
        AddGrpc(serviceCollection, configuration);

        serviceCollection
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
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

    public static void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAuthProvider, AuthProvider>();
    }

    public static void AddGrpc(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddGrpc();
        serviceCollection.AddGrpcReflection();

        serviceCollection.AddGrpcClient<AuthGrpc.AuthGrpcClient>(options =>
        {
            var url = configuration.GetValue<string>("AUTH_ADDRESS");

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Требуется указать переменную окружения AUTH_ADDRESS или она пустая");
            }

            options.Address = new Uri(url);
        });
    }
}

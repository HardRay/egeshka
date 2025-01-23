using Egeshka.ApiGateway.Providers;
using Egeshka.ApiGateway.Providers.Interfaces;
using Egeshka.Auth.Grpc;

namespace Egeshka.ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddTransient<IAuthProvider, AuthProvider>();

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection ConfigureGrpc(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddGrpcReflection();
        services.AddGrpcClients(configuration);

        return services;
    }

    private static IServiceCollection AddGrpcClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcClient<AuthGrpc.AuthGrpcClient>(options =>
        {
            var url = configuration.GetValue<string>("AUTH_ADDRESS");

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Требуется указать переменную окружения AUTH_ADDRESS или она пустая");
            }

            options.Address = new Uri(url);
        });

        return services;
    }
}

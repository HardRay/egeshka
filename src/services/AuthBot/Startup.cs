using Egeshka.Auth.Grpc;
using Egeshka.AuthBot.BackgroundJobs;
using Egeshka.AuthBot.Providers;
using Egeshka.AuthBot.Providers.Interfaces;
using Egeshka.AuthBot.Services;

namespace Egeshka.AuthBot;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        AddServices(serviceCollection);
        AddGrpc(serviceCollection, configuration);

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
                endpointRouteBuilder.MapGet("", () => "Hello Wold!");
            });
    }

    public static void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITelegramService, TelegramService>();
        serviceCollection.AddTransient<IAuthProvider, AuthProvider>();

        serviceCollection.AddHostedService<TelegramBackgroundService>();
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

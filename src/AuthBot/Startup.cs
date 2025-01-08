using Egeshka.AuthBot.BackgroundJobs;
using Egeshka.AuthBot.Services;

namespace Egeshka.AuthBot;

public class Startup()
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        AddServices(serviceCollection);

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

        serviceCollection.AddHostedService<TelegramBackgroundService>();
    }
}

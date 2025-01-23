using Egeshka.AuthBot.Services;

namespace Egeshka.AuthBot.BackgroundJobs;

public sealed class TelegramBackgroundService(ITelegramService telegramService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await telegramService.StartAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}

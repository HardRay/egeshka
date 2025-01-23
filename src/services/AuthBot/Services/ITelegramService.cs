namespace Egeshka.AuthBot.Services;

public interface ITelegramService
{
    Task StartAsync(CancellationToken cancellationToken);
}

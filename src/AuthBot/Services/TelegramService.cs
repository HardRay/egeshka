using Egeshka.AuthBot.Constants;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Egeshka.AuthBot.Services;

public sealed class TelegramService(IConfiguration configuration, ILogger<TelegramService> logger) : ITelegramService
{
    private readonly ITelegramBotClient _botClient = new TelegramBotClient(GetBotToken(configuration));
    private readonly ReceiverOptions _receiverOptions = new() { AllowedUpdates = [UpdateType.Message] };

    private static string GetBotToken(IConfiguration configuration)
    {
        const string EnvOption = "BOT_TOKEN";

        var botToken = configuration.GetValue<string>(EnvOption);
        if (string.IsNullOrEmpty(botToken))
        {
            throw new ArgumentException($"Требуется указать переменную окружения {EnvOption} или она пустая");
        }

        return botToken;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, _receiverOptions, cancellationToken);

        var bot = await _botClient.GetMe(cancellationToken);
        logger.LogInformation("Бот {BotName} запущен!", bot.FirstName);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message when update.Message is not null:
                {
                    await HandleMessageAsync(update.Message, cancellationToken);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "При обработке сообщения возникло исключение");
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken)
    {
        logger.LogError(ex, "При работе бота возникло исключение");
        return Task.CompletedTask;
    }

    private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
    {
        if (message.Text == "/start")
        {
            await HandleStartMessageAsync(message, cancellationToken);
            return;
        }

        if (message.Contact is not null)
        {
            await HandleContactAsync(message, cancellationToken);
            return;
        }

        await HandleUnknownMessageAsync(message, cancellationToken);
    }

    private async Task HandleStartMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        await _botClient.SendMessage(chatId, BotMessages.Greeting, replyMarkup: GetContactKeyboard(), cancellationToken: cancellationToken);
    }

    private Task HandleContactAsync(Message message, CancellationToken cancellationToken)
    {
        if (message.Contact is null)
        {
            logger.LogError("Не указан контакт пользователя");
            return Task.CompletedTask;
        }

        logger.LogInformation("Пользователь прислал свой контакт! Номер: {PhoneNumber}", message.Contact.PhoneNumber);


        return Task.CompletedTask;
    }

    private async Task HandleUnknownMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        await _botClient.SendMessage(chatId, BotMessages.Unknown, replyMarkup: GetContactKeyboard(), cancellationToken: cancellationToken);
    }

    private static ReplyKeyboardMarkup GetContactKeyboard()
    {
        return new ReplyKeyboardMarkup(
            new List<KeyboardButton[]>()
            {
                new KeyboardButton[]
                {
                    KeyboardButton.WithRequestContact("Отправить контакт"),
                },
            })
        {
            ResizeKeyboard = true,
        };
    }
}

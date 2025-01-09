using Egeshka.AuthBot.Constants;
using Egeshka.AuthBot.Mappers;
using Egeshka.AuthBot.Models;
using Egeshka.AuthBot.Providers.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Egeshka.AuthBot.Services;

public sealed class TelegramService(
    IConfiguration configuration,
    ILogger<TelegramService> logger,
    IAuthProvider authProvider) : ITelegramService
{
    private readonly ITelegramBotClient _botClient = new TelegramBotClient(GetBotToken(configuration));
    private readonly ReceiverOptions _receiverOptions = new() { AllowedUpdates = [UpdateType.Message] };

    private readonly string _appLink = GetAppLink(configuration);
    private const string RegistrationTokenAlias = "{RegistrationToken}";

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

    private static string GetAppLink(IConfiguration configuration)
    {
        const string Option = "AppLink";

        var appLink = configuration.GetValue<string>(Option);
        if (string.IsNullOrEmpty(appLink))
        {
            throw new ArgumentException($"Требуется указать настройку {Option} или она пустая");
        }

        return appLink;
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

        await _botClient.SendMessage(chatId,
            BotMessages.Greeting,
            replyMarkup: GetContactKeyboard(),
            cancellationToken: cancellationToken);
    }

    private async Task HandleContactAsync(Message message, CancellationToken cancellationToken)
    {
        if (message.Contact is null)
        {
            logger.LogError("Не указан контакт пользователя");
            return;
        }

        var chatId = message.Chat.Id;
        var registrationModel = message.Contact.ToRegistrationModel(chatId);
        var registrationResult = await authProvider.RegistrationAsync(registrationModel, cancellationToken);

        if (registrationResult is null)
        {
            await _botClient.SendMessage(chatId,
                BotMessages.RegistrationError,
                replyMarkup: GetContactKeyboard(),
                cancellationToken: cancellationToken);

            return;
        }

        await _botClient.SendMessage(chatId,
                BotMessages.Login,
                replyMarkup: GetLoginKeyboard(registrationResult.RegistrationToken),
                cancellationToken: cancellationToken);
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

    private InlineKeyboardMarkup GetLoginKeyboard(string registrationToken)
    {
        var deepLink = _appLink.Replace(RegistrationTokenAlias, registrationToken);
        return new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Войти", deepLink),
                },
            });
    }
}

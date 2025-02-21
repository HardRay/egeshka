namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct TelegramUserId(long Value)
{
    public static implicit operator long(TelegramUserId telegramUserId) => telegramUserId.Value;
}

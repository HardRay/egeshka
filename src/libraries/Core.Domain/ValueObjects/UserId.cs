namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct UserId(long Value)
{
    public static implicit operator long(UserId telegramUserId) => telegramUserId.Value;
}


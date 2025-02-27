﻿namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct SessionId(long Value)
{
    public static implicit operator long(SessionId telegramUserId) => telegramUserId.Value;
}

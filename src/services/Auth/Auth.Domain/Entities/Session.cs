using Egeshka.Core.Domain.ValueObjects;

namespace Egeshka.Auth.Domain.Entities;

public sealed record Session(
    SessionId Id,
    UserId UserId,
    string AccessToken,
    string RefreshToken,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdateAt);

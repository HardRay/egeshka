namespace Egeshka.Auth.Application.Options;

public sealed class AccessTokenOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    public int ExpirationInMinutes { get; init; }
}

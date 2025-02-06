namespace Egeshka.ApiGateway.Options;

public sealed class AuthOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
}
namespace Egeshka.Auth.Application.Models;

public sealed record LoginResult(string AccessToken, string RefreshToken);
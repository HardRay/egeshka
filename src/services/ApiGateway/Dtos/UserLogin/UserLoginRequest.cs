namespace Egeshka.ApiGateway.Dtos.UserLogin;

/// <summary>
/// Запрос получения токена доступа
/// </summary>
public sealed class UserLoginRequest()
{
    /// <summary>
    /// Токен регистрации
    /// </summary>
    public string? RegistrationToken { get; init; }

    /// <summary>
    /// Токен обновления
    /// </summary>
    public string? RefreshToken { get; init; }
}

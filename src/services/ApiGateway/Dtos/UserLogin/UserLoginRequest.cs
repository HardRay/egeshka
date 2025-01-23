using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.UserLogin;

/// <summary>
/// Запрос получения токена доступа
/// </summary>
public sealed class UserLoginRequest()
{
    /// <summary>
    /// Токен регистрации
    /// </summary>
    [Required]
    public required string RegistrationToken { get; init; }
}

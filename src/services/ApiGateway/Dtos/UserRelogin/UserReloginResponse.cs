using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.UserRelogin;

/// <summary>
/// Ответ обновления токена доступа
/// </summary>
public sealed class UserReloginResponse
{
    /// <summary>
    /// Токен доступа
    /// </summary>
    [Required]
    public required string AccessToken { get; init; }

    /// <summary>
    /// Токен обновления
    /// </summary>
    [Required]
    public required string RefreshToken { get; init; }
}
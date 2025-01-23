using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.UserRelogin;

/// <summary>
/// Запрос обновления токена доступа
/// </summary>
public sealed class UserReloginRequest
{
    /// <summary>
    /// Токен обновления
    /// </summary>
    [Required]
    public required string RefreshToken { get; init; }
}

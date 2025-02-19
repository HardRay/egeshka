﻿using System.ComponentModel.DataAnnotations;

namespace Egeshka.ApiGateway.Dtos.UserLogin;

/// <summary>
/// Ответ получения токена доступа
/// </summary>
public sealed class UserLoginResponse
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

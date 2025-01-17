using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Egeshka.ApiGateway.Dtos.UserLogin;

/// <summary>
/// Ответ получения токена доступа
/// </summary>
public sealed class UserLoginResponse
{
    /// <summary>
    /// Токен доступа
    /// </summary>
    public required string AccessToken { get; init; }

    /// <summary>
    /// Токен обновления
    /// </summary>
    public required string RefreshToken { get; init; }

    public static UserLoginResponse Generate()
    {
        return new UserLoginResponse()
        {
            AccessToken = GenerateJwtToken(),
            RefreshToken = GenerateToken()
        };
    }

    private static string GenerateToken()
    {
        var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var key = Guid.NewGuid().ToByteArray();
        var token = Convert.ToBase64String(time.Concat(key).ToArray());
        return token;
    }

    private static string GenerateJwtToken()
    {
        var claims = new List<Claim> { };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
               expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
           signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private sealed class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!qq";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}

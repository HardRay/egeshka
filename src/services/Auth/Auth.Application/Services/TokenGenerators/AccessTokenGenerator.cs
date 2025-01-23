using Egeshka.Auth.Application.Services.TokenGenerators.Interfaces;
using Egeshka.Auth.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Egeshka.Auth.Application.Services.TokenGenerators;

public class AccessTokenGenerator : IAccessTokenGenerator
{
    public string GenerateToken(UserId userId)
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

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new(Encoding.UTF8.GetBytes(KEY));
    }
}

using Egeshka.Auth.Application.Options;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Application.Services.TokenProviders.Interfaces;
using Egeshka.Auth.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Egeshka.Auth.Application.Services.TokenProviders;

public class AccessTokenProvider(
    IOptions<AccessTokenOptions> authOptions,
    IDateTimeProvider dateTimeProvider) : IAccessTokenProvider
{
    public string GenerateToken(UserId userId)
    {
        var secretKey = authOptions.Value.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
            ]),
            Expires = dateTimeProvider.UtcNow.AddMinutes(authOptions.Value.ExpirationInMinutes).DateTime,
            SigningCredentials = credentials,
            Issuer = authOptions.Value.Issuer,
            Audience = authOptions.Value.Audience
        };

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}

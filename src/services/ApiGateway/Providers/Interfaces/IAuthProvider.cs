using Egeshka.ApiGateway.Dtos.UserLogin;
using Egeshka.ApiGateway.Dtos.UserRelogin;

namespace Egeshka.ApiGateway.Providers.Interfaces;

public interface IAuthProvider
{
    Task<UserLoginResponse> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken);
    Task<UserReloginResponse> ReloginAsync(UserReloginRequest request, CancellationToken cancellationToken);
}

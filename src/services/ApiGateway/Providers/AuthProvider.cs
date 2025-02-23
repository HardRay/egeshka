using Egeshka.ApiGateway.Dtos.UserLogin;
using Egeshka.ApiGateway.Dtos.UserRelogin;
using Egeshka.ApiGateway.Mappers;
using Egeshka.ApiGateway.Providers.Interfaces;
using Egeshka.Grpc.Auth;

namespace Egeshka.ApiGateway.Providers;

public class AuthProvider(AuthGrpc.AuthGrpcClient authClient) : IAuthProvider
{
    public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var response = await authClient.LoginAsync(request.ToProto(), cancellationToken: cancellationToken);

        return response.ToService();
    }

    public async Task<UserReloginResponse> ReloginAsync(UserReloginRequest request, CancellationToken cancellationToken)
    {
        var response = await authClient.ReloginAsync(request.ToProto(), cancellationToken: cancellationToken);

        return response.ToService();
    }
}

using Egeshka.ApiGateway.Dtos.UserLogin;
using Egeshka.ApiGateway.Dtos.UserRelogin;
using Egeshka.Grpc.Auth;

namespace Egeshka.ApiGateway.Mappers;

public static class AuthMapper
{
    public static Login.Types.Request ToProto(this UserLoginRequest request)
    {
        return new Login.Types.Request()
        {
            RegistrationToken = request.RegistrationToken
        };
    }

    public static UserLoginResponse ToService(this Login.Types.Response response)
    {
        return new UserLoginResponse()
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken
        };
    }

    public static Relogin.Types.Request ToProto(this UserReloginRequest request)
    {
        return new Relogin.Types.Request()
        {
            RefreshToken = request.RefreshToken
        };
    }

    public static UserReloginResponse ToService(this Relogin.Types.Response response)
    {
        return new UserReloginResponse()
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken
        };
    }
}

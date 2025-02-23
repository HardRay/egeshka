using Egeshka.Auth.Application.Models;
using Egeshka.AuthBot.Mappers;
using Egeshka.AuthBot.Models;
using Egeshka.AuthBot.Providers.Interfaces;
using Egeshka.Grpc.Auth;

namespace Egeshka.AuthBot.Providers;

public class AuthProvider(AuthGrpc.AuthGrpcClient authClient) : IAuthProvider
{
    public async Task<RegistrationResult> RegistrationAsync(RegistrationModel model, CancellationToken cancellationToken)
    {
        var response = await authClient.RegistrationAsync(model.ToProto(), cancellationToken: cancellationToken);

        return response.ToServiceModel();
    }
}

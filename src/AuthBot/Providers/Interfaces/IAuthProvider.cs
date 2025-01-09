using Egeshka.Auth.Application.Models;
using Egeshka.AuthBot.Models;

namespace Egeshka.AuthBot.Providers.Interfaces;

public interface IAuthProvider
{
    Task<RegistrationResult> RegistrationAsync(RegistrationModel model, CancellationToken cancellationToken);
}

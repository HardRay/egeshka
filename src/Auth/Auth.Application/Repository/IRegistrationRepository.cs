using Egeshka.Auth.Application.Models;

namespace Egeshka.Auth.Application.Repository;

public interface IRegistrationRepository
{
    Task RegistrationAsync(RegistrationModel model, CancellationToken cancellationToken);
}

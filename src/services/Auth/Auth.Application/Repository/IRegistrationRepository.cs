using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Models.Repository;

namespace Egeshka.Auth.Application.Repository;

public interface IRegistrationRepository
{
    Task InsertAsync(RegistrationInsertModel model, CancellationToken cancellationToken);

    Task<RegistrationModel?> GetByTokenAsync(string token, CancellationToken cancellationToken);
}

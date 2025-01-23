using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Domain.Entities;

namespace Egeshka.Auth.Application.Repository;

public interface IRegistrationRepository
{
    Task InsertAsync(RegistrationInsertModel model, CancellationToken cancellationToken);
    Task<Registration?> GetByTokenAsync(string token, CancellationToken cancellationToken);
    Task<Registration?> GetByIdAsync(long id, CancellationToken cancellationToken);
}

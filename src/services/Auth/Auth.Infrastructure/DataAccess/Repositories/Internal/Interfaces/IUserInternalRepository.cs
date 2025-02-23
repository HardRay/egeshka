using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Domain.Entities;
using Egeshka.Core.Domain.ValueObjects;
using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Internal.Interfaces;

public interface IUserInternalRepository
{
    Task<long> InsertUserAsync(
        NpgsqlConnection connection,
        UserInsertModel user,
        CancellationToken cancellationToken);

    Task<User?> GetUserByMobileNumberAsync(
        NpgsqlConnection connection,
        MobileNumber mobileNumber,
        CancellationToken cancellationToken);
}

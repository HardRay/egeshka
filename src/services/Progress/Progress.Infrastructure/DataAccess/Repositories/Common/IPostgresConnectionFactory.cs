using Npgsql;

namespace Egeshka.Progress.Infrastructure.DataAccess.Repositories.Common;

/// <summary>
/// Фабрика подключения к Postgres БД
/// </summary>
public interface IPostgresConnectionFactory
{
    /// <summary>
    /// Получение подключения к БД
    /// </summary>
    /// <returns>Подключение к БД</returns>
    NpgsqlConnection GetConnection();
}

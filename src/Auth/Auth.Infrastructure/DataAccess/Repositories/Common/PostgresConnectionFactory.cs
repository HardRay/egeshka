﻿using Npgsql;

namespace Egeshka.Auth.Infrastructure.DataAccess.Repositories.Common;

/// <inheritdoc/>
public class PostgresConnectionFactory(
    string connectionString) : IPostgresConnectionFactory
{
    /// <inheritdoc/>
    public NpgsqlConnection GetConnection() => new(connectionString);
}
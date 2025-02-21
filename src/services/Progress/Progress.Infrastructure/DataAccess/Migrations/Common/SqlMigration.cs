using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace Egeshka.Progress.Infrastructure.DataAccess.Migrations.Common;

/// <summary>
/// Мигратор для SQL-миграций
/// </summary>
public abstract class SqlMigration : IMigration
{
    public void GetUpExpressions(IMigrationContext context)
    {
        _ = context ?? throw new ArgumentNullException(nameof(context));

        context.Expressions.Add(new ExecuteSqlStatementExpression { SqlStatement = GetUpSql(context.ServiceProvider) });
    }

    public void GetDownExpressions(IMigrationContext context)
    {
        _ = context ?? throw new ArgumentNullException(nameof(context));

        context.Expressions.Add(new ExecuteSqlStatementExpression { SqlStatement = GetDownSql(context.ServiceProvider) });
    }

    /// <summary>
    /// Получение SQL-скрипта для выполнения миграции
    /// </summary>
    protected abstract string GetUpSql(IServiceProvider services);

    /// <summary>
    /// Получение SQL-скрипта для отката миграции
    /// </summary>
    protected abstract string GetDownSql(IServiceProvider services);

    string IMigration.ConnectionString => throw new NotSupportedException();
}

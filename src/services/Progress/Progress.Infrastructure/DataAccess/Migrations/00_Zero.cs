using FluentMigrator;

namespace Egeshka.Progress.Infrastructure.DataAccess.Migrations;

/// <summary>
/// Миграция для проверки выполнения миграций
/// </summary>
[Migration(0, "Zero migration")]
public class Zero : Migration
{
    /// <inheritdoc/>
    public override void Up()
    {
    }

    /// <inheritdoc/>
    public override void Down()
    {
    }
}
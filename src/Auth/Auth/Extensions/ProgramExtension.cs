using FluentMigrator.Runner;

namespace Egeshka.Auth.Extensions;

/// <summary>
/// Расширения для <see cref="Program"/>
/// </summary>
public static class ProgramExtension
{
    /// <summary>
    /// Запуск приложения или миграции
    /// </summary>
    /// <param name="host">Хост</param>
    /// <param name="args">Аргументы</param>
    public static Task RunOrMigrateAsync(
        this IHost host,
        string[] args)
    {
        if (!IsNeedMigration(args))
        {
            return host.RunAsync();
        }

        var scoppe = host.Services.CreateScope();
        var runner = scoppe.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();

        return Task.CompletedTask;
    }

    private static bool IsNeedMigration(string[] args)
    {
        return args is { Length: > 0 } && args[0] == "migrate";
    }
}

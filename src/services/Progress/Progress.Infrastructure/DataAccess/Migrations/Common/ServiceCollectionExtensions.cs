﻿using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Egeshka.Progress.Infrastructure.DataAccess.Migrations.Common;

/// <summary>
/// Расширения коллекции сервисов в DI для добавления миграций
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление миграций
    /// </summary>
    public static IServiceCollection AddFluentMigrator(
        this IServiceCollection collection,
        string connectionString)
    {
        var assembly = Assembly.GetExecutingAssembly();

        collection.AddFluentMigratorCore()
            .ConfigureRunner(
                builder => builder
                    .AddPostgres()
                    .ScanIn(assembly)
                    .For.Migrations())
            .AddOptions<ProcessorOptions>()
            .Configure(
                options =>
                {
                    options.ConnectionString = connectionString;
                    options.Timeout = TimeSpan.FromMinutes(1);
                    options.ProviderSwitches = "Force Quote=false";
                });

        return collection;
    }
}

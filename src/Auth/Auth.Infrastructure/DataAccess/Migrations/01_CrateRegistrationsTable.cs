﻿using Egeshka.Auth.Infrastructure.DataAccess.Migrations.Common;
using FluentMigrator;

namespace Egeshka.Auth.Infrastructure.DataAccess.Migrations;

/// <summary>
/// Миграция с созданием таблицы регистраций
/// </summary>
[Migration(1, "Create registration table")]
public sealed class CreateRegistrationsTable : SqlMigration
{
    protected override string GetUpSql(IServiceProvider services) =>
        """
            create table if not exists registrations(
                id bigserial primary key,
                telegram_user_id bigint not null,
                mobile_number varchar(20) not null,
                first_name varchar(100) not null,
                last_name varchar(100) null,
        	    create_at timestamp with time zone not null
            );
        """;

    protected override string GetDownSql(IServiceProvider services) =>
        """
            drop table if exists registrations;
        """;
}

using Egeshka.Auth.Infrastructure.DataAccess.Migrations.Common;
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
                registration_token varchar(200) not null,
        	    create_at timestamp with time zone not null
            );

            create index registrations_registration_token_idx on registrations(registration_token);
            create index registrations_telegram_user_id_idx on registrations(telegram_user_id);
        """;

    protected override string GetDownSql(IServiceProvider services) =>
        """
            drop table if exists registrations;
        """;
}

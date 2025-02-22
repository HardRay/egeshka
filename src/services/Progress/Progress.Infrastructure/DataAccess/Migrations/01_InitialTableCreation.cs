using Egeshka.Progress.Infrastructure.DataAccess.Migrations.Common;
using FluentMigrator;

namespace Egeshka.Progress.Infrastructure.DataAccess.Migrations;

/// <summary>
/// Миграция с созданием таблиц прогресса пользователя
/// </summary>
[Migration(1, "Initial table creation")]
public sealed class InitialTableCreation : SqlMigration
{
    protected override string GetUpSql(IServiceProvider services) =>
        """
            create table if not exists exercise_results(
                id bigserial primary key,
                user_id bigint not null,
                subject_id bigint not null,
                exercise_id bigint not null,
                error_task_ids bigint[] not null,
                experience_points int not null,
        	    create_at timestamp with time zone not null
            );

            create index exercise_results_user_idx on exercise_results(user_id);
            create index exercise_results_subject_exercise_idx on exercise_results(subject_id, exercise_id);

            create table if not exists streak_items(
                user_id bigint not null,
                date date not null,
                type int not null,
                primary key (user_id, date)
            );
        """;

    protected override string GetDownSql(IServiceProvider services) =>
        """
            drop table if exists exercise_results;
            drop table if exists streak_items;
        """;
}

using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Egeshka.Progress.Infrastructure.DataAccess.TypeHandlers;

public sealed class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        return value is DateOnly dateOnly
            ? dateOnly
            : DateOnly.FromDateTime((DateTime)value);
    }

    public override void SetValue([DisallowNull] IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        parameter.DbType = DbType.Date;
    }
}
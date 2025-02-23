using Egeshka.Grpc.Common;

namespace Egeshka.Proto.Common.Mappers;

public static class DateMapper
{
    public static DateOnly ToServiceModel(this Date date)
    {
        return new DateOnly(date.Year, date.Month, date.Day);
    }

    public static Date ToProto(this DateOnly date)
    {
        return new Date()
        {
            Year = date.Year,
            Month = date.Month,
            Day = date.Day
        };
    }
}

using Egeshka.Core.Domain.ValueObjects;
using Egeshka.Grpc.Progress;
using Egeshka.Progress.Application.Queries.GetUserStreak;
using Egeshka.Progress.Domain.Entities;
using Egeshka.Proto.Common.Mappers;
using Egeshka.Proto.Progress.Mappers;

namespace Egeshka.Progress.Hosting.Mappers;

public static class StreakMapper
{
    public static GetUserStreakQuery ToServiceQuery(this GetUserStreak.Types.Request request)
    {
        return new GetUserStreakQuery(new UserId(request.UserId));
    }

    public static GetUserStreak.Types.Response ToProto(this GetUserStreakResult result)
    {
        return new GetUserStreak.Types.Response()
        {
            CurrentStreak = result.CurrentStreak,
            Items = { result.StreakItems.Select(x => x.ToProto()) }
        };
    }

    public static GetUserStreak.Types.StreakItem ToProto(this StreakItem item)
    {
        return new GetUserStreak.Types.StreakItem()
        {
            Date = item.Date.ToProto(),
            Type = item.Type.ToProto()
        };
    }
}

using Egeshka.ApiGateway.Dtos.Progress.GetMyStreaks;
using Egeshka.Grpc.Progress;
using Egeshka.Proto.Common.Mappers;
using Egeshka.Proto.Progress.Mappers;

namespace Egeshka.ApiGateway.Mappers;

public static class StreakMapper
{
    public static GetMyStreaksResponse ToService(this GetUserStreak.Types.Response response)
    {
        return new GetMyStreaksResponse()
        {
            CurrentStreak = response.CurrentStreak,
            Streaks = response.Items.Select(i => i.ToService()).ToArray()
        };
    }

    public static StreakItem ToService(this GetUserStreak.Types.StreakItem item)
    {
        return new StreakItem()
        {
            Date = item.Date.ToServiceModel(),
            Type = item.Type.ToService()
        };
    }
}

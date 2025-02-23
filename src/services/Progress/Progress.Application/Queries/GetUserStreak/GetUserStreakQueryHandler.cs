using Egeshka.Core.Application.Services.Interfaces;
using Egeshka.Progress.Application.Repositories;
using Egeshka.Progress.Domain.Entities;
using MediatR;

namespace Egeshka.Progress.Application.Queries.GetUserStreak;

public sealed class GetUserStreakQueryHandler(
    IStreakRepository repository,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<GetUserStreakQuery, GetUserStreakResult>
{
    public async Task<GetUserStreakResult> Handle(GetUserStreakQuery request, CancellationToken cancellationToken)
    {
        var streakItems = await repository.GetUserStreakAsync(request.UserId, cancellationToken);
        var sortedStreakItems = streakItems.OrderByDescending(x => x.Date).ToArray();
        var currentStreak = GetCurrentStreak(streakItems);

        return new GetUserStreakResult(currentStreak, sortedStreakItems);
    }

    private int GetCurrentStreak(IReadOnlyCollection<StreakItem> items)
    {
        if (items.Count == 0)
            return 0;

        var dates = items.Select(i => i.Date).ToArray();

        var currentDate = DateOnly.FromDateTime(dateTimeProvider.Now.Date);
        if (dates.First() != currentDate)
            return 0;

        var streak = 1;
        for (int i = 1; i < items.Count; i++)
        {
            var offset = dates[i].DayNumber - dates[i - 1].DayNumber;
            if (offset != 1)
                return streak;
            streak++;
        }

        return streak;
    }
}

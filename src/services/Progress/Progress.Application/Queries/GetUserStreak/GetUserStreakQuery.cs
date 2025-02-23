using Egeshka.Core.Domain.ValueObjects;
using MediatR;

namespace Egeshka.Progress.Application.Queries.GetUserStreak;

public sealed record GetUserStreakQuery(UserId UserId) : IRequest<GetUserStreakResult>;

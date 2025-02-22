using Egeshka.Core.Domain.ValueObjects;
using MediatR;

namespace Egeshka.Progress.Application.Queries.GetCompletedExercises;

public sealed record GetCompletedExercisesQuery(UserId UserId, SubjectId SubjectId)
    : IRequest<GetCompletedExercisesResult>;

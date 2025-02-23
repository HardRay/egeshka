using Egeshka.Progress.Application.Repositories;
using MediatR;

namespace Egeshka.Progress.Application.Queries.GetCompletedExercises;

public sealed class GetCompletedExercisesQueryHandler(
    IExerciseResultRepository repository)
    : IRequestHandler<GetCompletedExercisesQuery, GetCompletedExercisesResult>
{
    public async Task<GetCompletedExercisesResult> Handle(GetCompletedExercisesQuery request, CancellationToken cancellationToken)
    {
        var exerciseIds = await repository.GetCompletedExercisesAsync(request.UserId, request.SubjectId, cancellationToken);
        var sortedExerciseIds = exerciseIds.DistinctBy(id => id.Value).OrderBy(id => id.Value).ToArray();

        return new GetCompletedExercisesResult(sortedExerciseIds);
    }
}

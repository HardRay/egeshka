using Egeshka.Progress.Application.Repositories;
using MediatR;

namespace Egeshka.Progress.Application.Queries.GetCompletedExercises;

public sealed class GetCompletedExercisesQueryHandler(
    IExerciseResultRepository repository)
    : IRequestHandler<GetCompletedExercisesQuery, GetCompletedExercisesResult>
{
    public async Task<GetCompletedExercisesResult> Handle(GetCompletedExercisesQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetCompletedExercises(request.UserId, request.SubjectId, cancellationToken);

        return new GetCompletedExercisesResult(result);
    }
}

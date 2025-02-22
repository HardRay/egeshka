using Egeshka.Progress.Application.Mappers;
using Egeshka.Progress.Application.Repositories;
using MediatR;

namespace Egeshka.Progress.Application.Commands;

public sealed class SaveExerciseResultCommandHandler(
    IExerciseResultRepository repository)
    : IRequestHandler<SaveExerciseResultCommand>
{
    public Task Handle(SaveExerciseResultCommand request, CancellationToken cancellationToken)
    {
        return repository.InsertAsync(request.ToInsertModel(), cancellationToken);
    }
}

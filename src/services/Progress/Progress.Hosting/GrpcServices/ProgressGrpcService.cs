using Egeshka.Progress.Grpc;
using Egeshka.Progress.Hosting.Mappers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace Egeshka.Progress.Hosting.GrpcServices;

public sealed class ProgressGrpcService(IMediator mediator) : ProgressGrpc.ProgressGrpcBase
{
    public override async Task<Empty> SaveExerciseResult(
        SaveExerciseResult.Types.Request request, ServerCallContext context)
    {
        await mediator.Send(request.ToServiceCommand(), cancellationToken: context.CancellationToken);

        return new Empty();
    }
}
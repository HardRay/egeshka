using Egeshka.Grpc.Progress;
using Egeshka.Progress.Hosting.Mappers;
using Grpc.Core;
using MediatR;

namespace Egeshka.Progress.Hosting.GrpcServices;

public sealed class StreakGprcService(IMediator mediator) : StreakGrpc.StreakGrpcBase
{
    public override async Task<GetUserStreak.Types.Response> GetUserStreak(
        GetUserStreak.Types.Request request,
        ServerCallContext context)
    {
        var result = await mediator.Send(request.ToServiceQuery(), cancellationToken: context.CancellationToken);

        return result.ToProto();
    }
}

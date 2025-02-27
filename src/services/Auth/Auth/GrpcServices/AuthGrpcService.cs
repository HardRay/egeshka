﻿using Egeshka.Auth.Mappers;
using Egeshka.Grpc.Auth;
using Grpc.Core;
using MediatR;

namespace Egeshka.Auth.GrpcServices;

public sealed class AuthGrpcService(IMediator mediator) : AuthGrpc.AuthGrpcBase
{
    public override async Task<Registration.Types.Response> Registration(
        Registration.Types.Request request, ServerCallContext context)
    {
        var result = await mediator.Send(request.ToServiceCommand(), cancellationToken: context.CancellationToken);

        return result.ToProto();
    }

    public override async Task<Login.Types.Response> Login(
        Login.Types.Request request, ServerCallContext context)
    {
        var result = await mediator.Send(request.ToServiceCommand(), cancellationToken: context.CancellationToken);

        return result.ToProto();
    }

    public override async Task<Relogin.Types.Response> Relogin(
        Relogin.Types.Request request, ServerCallContext context)
    {
        var result = await mediator.Send(request.ToServiceCommand(), cancellationToken: context.CancellationToken);

        return result.ToProto();
    }
}

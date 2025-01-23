using Egeshka.Auth.Application.Commands.Relogin;
using MediatR;

namespace Egeshka.Auth.Application.Handlers;

public sealed class ReloginHandler : IRequestHandler<ReloginCommand, ReloginResult>
{
    public async Task<ReloginResult> Handle(ReloginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

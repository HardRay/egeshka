using Egeshka.Auth.Application.Commands.Refresh;
using Egeshka.Auth.Application.Models;
using MediatR;

namespace Egeshka.Auth.Application.Handlers;

public sealed class RefreshHandler : IRequestHandler<RefreshCommand, LoginResult>
{
    public async Task<LoginResult> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using Egeshka.Auth.Application.Commands.Relogin;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Core.Models.Exceptions;
using MediatR;

namespace Egeshka.Auth.Application.Handlers;

public sealed class ReloginHandler(
    IUserRepository userRepository,
    IAuthorizationService authorizationService)
    : IRequestHandler<ReloginCommand, ReloginResult>
{
    public async Task<ReloginResult> Handle(ReloginCommand request, CancellationToken cancellationToken)
    {
        var session = await userRepository.GetSessionByRefreshToken(request.RefreshToken, cancellationToken)
            ?? throw new EntityNotFoundException("Сессия не найдена");

        var authorizationData = await authorizationService.GenerateAuthorizationDataAsync(session.UserId, cancellationToken);

        var updateSession = new SessionUpdateModel(session.Id, authorizationData.RefreshToken);
        await userRepository.UpdateSessionAsync(updateSession, cancellationToken);

        return new ReloginResult(authorizationData);
    }
}

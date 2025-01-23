using Egeshka.Auth.Application.Commands.Login;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Core.Models.Exceptions;
using MediatR;

namespace Egeshka.Auth.Application.Handlers;

public sealed class LoginHandler(
    IRegistrationRepository registrationRepository,
    IUserRepository userRepository,
    IAuthorizationService authorizationService)
    : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var registration = await registrationRepository.GetByTokenAsync(request.RegistrationToken, cancellationToken)
            ?? throw new EntityNotFoundException("Не найдена регистрация");

        var userCreationResult = await userRepository.CreateUserByRegistrationIdAsync(registration.Id, cancellationToken);
        var userId = userCreationResult.UserId;

        var authorizationData = await authorizationService.GenerateAuthorizationDataAsync(userId, cancellationToken);

        var session = new SessionInsertModel(userId, authorizationData.AccessToken, authorizationData.RefreshToken);
        await userRepository.InsertSessionAsync(session, cancellationToken);

        return new LoginResult(authorizationData);
    }
}

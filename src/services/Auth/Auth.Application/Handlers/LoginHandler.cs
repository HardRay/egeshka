using Egeshka.Auth.Application.Commands.Login;
using Egeshka.Auth.Application.Models.Repository;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using Egeshka.Auth.Domain.Entities;
using Egeshka.Core.Domain.ValueObjects;
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

        var userId = await GetOrCreateUserIdAsync(registration, cancellationToken);
        var authorizationData = await authorizationService.GenerateAuthorizationDataAsync(userId, cancellationToken);

        var session = new SessionInsertModel(userId, authorizationData.RefreshToken);
        await userRepository.InsertSessionAsync(session, cancellationToken);

        return new LoginResult(authorizationData);
    }

    private async Task<UserId> GetOrCreateUserIdAsync(Registration registration, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetUserByMobileNumberAsync(registration.MobileNumber, cancellationToken);
        if (existingUser is not null)
            return existingUser.Id;

        var userCreationResult = await userRepository.CreateUserByRegistrationIdAsync(registration.Id, cancellationToken);
        
        return userCreationResult.UserId;
    }
}

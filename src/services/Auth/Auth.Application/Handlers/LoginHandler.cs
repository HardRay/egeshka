using Egeshka.Auth.Application.Commands.Login;
using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Repository;
using Egeshka.Core.Models.Exceptions;
using MediatR;

namespace Egeshka.Auth.Application.Handlers;

public sealed class LoginHandler(
    IRegistrationRepository registrationRepository)
    : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var registration = await registrationRepository.GetByTokenAsync(request.RegistrationToken, cancellationToken)
            ?? throw new EntityNotFoundException("Не найдена регистрация");
        throw new Exception();
    }
}

using Egeshka.Auth.Application.Commands.Registration;
using Egeshka.Auth.Application.Mappers;
using Egeshka.Auth.Application.Models;
using Egeshka.Auth.Application.Repository;
using Egeshka.Auth.Application.Services.Interfaces;
using MediatR;

namespace Egeshka.Auth.Application.Handlers;

public sealed class RegistrationHandler(
    IRegistrationTokenGenerator tokenGenerator,
    IRegistrationRepository repository)
    : IRequestHandler<RegistrationCommand, RegistrationResult>
{
    public async Task<RegistrationResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var registrationToken = tokenGenerator.GenerateToken();

        var registrationModel = request.ToModel(registrationToken);
        await repository.RegistrationAsync(registrationModel, cancellationToken);

        return new RegistrationResult(registrationToken);
    }
}

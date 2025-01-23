using Egeshka.Auth.Application.Models;
using MediatR;

namespace Egeshka.Auth.Application.Commands.Login;

public sealed record LoginCommand(string RegistrationToken) : IRequest<LoginResult>;

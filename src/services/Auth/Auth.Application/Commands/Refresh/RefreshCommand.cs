using Egeshka.Auth.Application.Models;
using MediatR;

namespace Egeshka.Auth.Application.Commands.Refresh;

public sealed record RefreshCommand(string RegistrationToken) : IRequest<LoginResult>;
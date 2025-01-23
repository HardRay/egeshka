using Egeshka.Auth.Application.Models;

namespace Egeshka.Auth.Application.Commands.Login;

public sealed record LoginResult(AuthorizationData Data);
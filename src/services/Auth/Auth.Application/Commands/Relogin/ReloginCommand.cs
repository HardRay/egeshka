using MediatR;

namespace Egeshka.Auth.Application.Commands.Relogin;

public sealed record ReloginCommand(string RefreshToken) : IRequest<ReloginResult>;
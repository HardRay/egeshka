using Egeshka.Auth.Application.Models;

namespace Egeshka.Auth.Application.Commands.Relogin;

public sealed record ReloginResult(AuthorizationData Data);

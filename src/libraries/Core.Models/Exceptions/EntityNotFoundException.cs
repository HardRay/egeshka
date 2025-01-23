using Egeshka.Core.Models.Constants;
using Egeshka.Core.Models.Exceptions.Common;
using Grpc.Core;
using System.Net;

namespace Egeshka.Core.Models.Exceptions;

public sealed class EntityNotFoundException(string message = "Сущность не найдена") : BaseException(message)
{
    public override string ErrorCode => ErrorCodes.EntityNotFound;

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public override StatusCode GrpcStatusCode =>StatusCode.NotFound;
}

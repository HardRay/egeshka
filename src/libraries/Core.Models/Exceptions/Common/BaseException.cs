using Grpc.Core;
using System.Net;

namespace Egeshka.Core.Models.Exceptions.Common;

public abstract class BaseException(string message) : Exception(message)
{
    public abstract string ErrorCode { get; }
    public abstract HttpStatusCode HttpStatusCode { get; }
    public abstract StatusCode GrpcStatusCode { get; }
}

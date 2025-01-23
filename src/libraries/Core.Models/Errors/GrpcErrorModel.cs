using Egeshka.Core.Models.Constants;
using System.Net;

namespace Egeshka.Core.Models.Errors;

public sealed class GrpcErrorModel
{
    public required string ErrorCode { get; init; }
    public IReadOnlyCollection<string>? ErrorMessages { get; init; }
    public HttpStatusCode HttpStatusCode { get; init; }

    public static GrpcErrorModel Create(string errorCode, HttpStatusCode httpStatusCode, string errorMessage)
    {
        return new GrpcErrorModel()
        {
            ErrorCode = errorCode,
            HttpStatusCode = httpStatusCode,
            ErrorMessages = [errorMessage]
        };
    }

    public static GrpcErrorModel GetUnknownError()
    {
        return new GrpcErrorModel()
        {
            ErrorCode = ErrorCodes.Unknown,
            HttpStatusCode = HttpStatusCode.InternalServerError,
            ErrorMessages = ["Необработанное исключение"]
        };
    }
}

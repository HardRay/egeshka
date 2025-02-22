using Egeshka.Core.Models.Constants;
using System.Net;

namespace Egeshka.Core.Models.Errors;

public sealed class ErrorModel
{
    public required string ErrorCode { get; init; }
    public IReadOnlyCollection<string>? ErrorMessages { get; init; }
    public HttpStatusCode HttpStatusCode { get; init; }

    public static ErrorModel Create(string errorCode, HttpStatusCode httpStatusCode, string errorMessage)
    {
        return new ErrorModel()
        {
            ErrorCode = errorCode,
            HttpStatusCode = httpStatusCode,
            ErrorMessages = [errorMessage]
        };
    }

    public static ErrorModel Create(string errorCode, HttpStatusCode httpStatusCode, IReadOnlyCollection<string> errorMessages)
    {
        return new ErrorModel()
        {
            ErrorCode = errorCode,
            HttpStatusCode = httpStatusCode,
            ErrorMessages = errorMessages
        };
    }

    public static ErrorModel GetUnknownError()
    {
        return new ErrorModel()
        {
            ErrorCode = ErrorCodes.Unknown,
            HttpStatusCode = HttpStatusCode.InternalServerError,
            ErrorMessages = ["Необработанное исключение"]
        };
    }
}

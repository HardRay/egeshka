using Egeshka.Core.Models.Errors;
using Egeshka.Core.Models.Exceptions.Common;

namespace Egeshka.Core.Hosting.Mappers;

public static class ErrorMapper
{
    public static GrpcErrorModel ToError(this BaseException exception)
        => GrpcErrorModel.Create(exception.ErrorCode, exception.HttpStatusCode, exception.Message);
}

using Egeshka.Core.Models.Errors;
using Egeshka.Core.Models.Exceptions.Common;
using Egeshka.Grpc.Common;
using System.Net;

namespace Egeshka.Proto.Common.Mappers;

public static class ErrorMapper
{
    public static ErrorModel ToError(this BaseException exception)
        => ErrorModel.Create(exception.ErrorCode, exception.HttpStatusCode, exception.Message);

    public static GrpcErrorModel ToProto(this ErrorModel model)
    {
        return new GrpcErrorModel()
        {
            Messages = { model.ErrorMessages },
            ErrorCode = model.ErrorCode,
            HttpStatusCode = (int)model.HttpStatusCode
        };
    }

    public static ErrorModel ToServiceModel(this GrpcErrorModel model)
    {
        return new ErrorModel()
        {
            ErrorCode = model.ErrorCode,
            ErrorMessages = model.Messages,
            HttpStatusCode = (HttpStatusCode)model.HttpStatusCode
        };
    }
}

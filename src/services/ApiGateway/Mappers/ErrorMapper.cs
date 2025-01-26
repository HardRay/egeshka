using Egeshka.ApiGateway.Dtos;
using Egeshka.Grpc.Common;

namespace Egeshka.ApiGateway.Mappers;

public static class ErrorMapper
{
    public static ErrorDto ToDto(this GrpcErrorModel model)
    {
        return new ErrorDto(model.ErrorCode, model.Messages);
    }
}

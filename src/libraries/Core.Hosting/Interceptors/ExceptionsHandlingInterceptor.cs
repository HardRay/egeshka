using Egeshka.Core.Hosting.Mappers;
using Egeshka.Core.Models.Errors;
using Egeshka.Core.Models.Exceptions.Common;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Egeshka.Core.Hosting.Interceptors;

public sealed class ExceptionsHandlingInterceptor : Interceptor
{
    public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            var response = await continuation(request, context);
            return response;
        }
        catch (BaseException ex)
        {
            var details = ex.ToError().ToJson();
            var status = new Status(ex.GrpcStatusCode, details, ex);
            throw new RpcException(status);
        }
        catch (Exception ex)
        {
            var details = GrpcErrorModel.GetUnknownError().ToJson();
            var status = new Status(StatusCode.Internal, details, ex);
            throw new RpcException(status);
        }
    }
}

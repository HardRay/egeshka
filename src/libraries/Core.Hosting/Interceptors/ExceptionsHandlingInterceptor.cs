using Egeshka.Core.Hosting.Mappers;
using Egeshka.Core.Models.Constants;
using Egeshka.Core.Models.Errors;
using Egeshka.Core.Models.Exceptions.Common;
using Google.Protobuf;
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
            var error = ex.ToError().ToProto();
            var metadata = new Metadata
            {
                { Headers.ErrorDetails, error.ToByteString().ToBase64() }
            };

            var status = new Status(ex.GrpcStatusCode, ex.Message, ex);
            throw new RpcException(status, metadata);
        }
        catch (Exception ex)
        {
            const string errorMessage = "Internal error";

            var error = ErrorModel.GetUnknownError().ToProto();
            var metadata = new Metadata
            {
                { Headers.ErrorDetails, error.ToByteString().ToBase64() }
            };

            var status = new Status(StatusCode.Internal, errorMessage, ex);
            throw new RpcException(status, metadata);
        }
    }
}

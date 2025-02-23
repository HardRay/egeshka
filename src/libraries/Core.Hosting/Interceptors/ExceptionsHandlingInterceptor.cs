using Egeshka.Core.Models.Constants;
using Egeshka.Core.Models.Errors;
using Egeshka.Core.Models.Exceptions.Common;
using Egeshka.Proto.Common.Mappers;
using FluentValidation;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Net;

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
        catch (ValidationException ex)
        {
            const string errorMessage = "Validation error";

            var errorMessages = ex.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").ToArray();
            var error = ErrorModel.Create(ErrorCodes.ValidationError, HttpStatusCode.BadRequest, errorMessages).ToProto();
            var metadata = new Metadata
            {
                { Headers.ErrorDetails, error.ToByteString().ToBase64() }
            };

            var status = new Status(StatusCode.InvalidArgument, errorMessage, ex);
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

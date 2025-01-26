using Egeshka.ApiGateway.Dtos;
using Egeshka.ApiGateway.Mappers;
using Egeshka.Core.Models.Constants;
using Egeshka.Core.Models.Errors;
using Egeshka.Grpc.Common;
using Grpc.Core;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Egeshka.ApiGateway.ExceptionHandlers;

public class RpcExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not RpcException rpcException)
            return false;

        var errorBin = rpcException.Trailers.GetValue(Headers.ErrorDetails);
        if (string.IsNullOrEmpty(errorBin))
            return false;

        var error = GrpcErrorModel.Parser.ParseFrom(Convert.FromBase64String(errorBin));

        const string contentType = "application/json";
        context.Response.ContentType = contentType;

        context.Response.StatusCode = error.HttpStatusCode;
        await context.Response.WriteAsync(error.ToDto().ToJson(), cancellationToken);

        return true;
    }
}

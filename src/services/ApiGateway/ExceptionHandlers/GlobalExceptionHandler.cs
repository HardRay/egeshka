using Egeshka.ApiGateway.Dtos;
using Egeshka.ApiGateway.Mappers;
using Egeshka.Core.Models.Constants;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Egeshka.ApiGateway.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        const string contentType = "application/json";
        context.Response.ContentType = contentType;

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorDto = ErrorDto.WithMessage(ErrorCodes.Unknown, "Неизвестная ошибка");
        await context.Response.WriteAsync(errorDto.ToJson(), cancellationToken);

        return true;
    }
}

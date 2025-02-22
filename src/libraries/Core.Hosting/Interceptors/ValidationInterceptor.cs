using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Egeshka.Core.Hosting.Interceptors;

public sealed class ValidationInterceptor(IServiceProvider serviceProvider) : Interceptor
{
    public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var validator = serviceProvider.GetService<AbstractValidator<TRequest>>();
        if (validator is not null)
            await validator.ValidateAndThrowAsync(request, context.CancellationToken);

        var response = await continuation(request, context);

        return response;
    }
}

namespace Egeshka.ApiGateway.Dtos;

public sealed class ErrorDto(string errorCode, IReadOnlyCollection<string>? errorMessages = null)
{
    public string ErrorCode { get; init; } = errorCode;
    public IReadOnlyCollection<string>? ErrorMessages { get; init; } = errorMessages;

    public static ErrorDto WithMessage(string errorCode, string errorMessage)
        => new(errorCode, errorMessages: [errorMessage]);
}

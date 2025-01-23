namespace Egeshka.ApiGateway.Dtos;

public sealed class ErrorDto
{
    public required string ErrorCode { get; init; }
    public IReadOnlyCollection<string>? ErrorMessages { get; init; }

    public static ErrorDto Create(string errorCode, string errorMessage)
    {
        return new ErrorDto()
        {
            ErrorCode = errorCode,
            ErrorMessages = [errorMessage]
        };
    }
}

namespace Egeshka.ApiGateway.Dtos;

public sealed class ProblemDetailsModel
{
    public required string ErrorCode { get; init; }
    public IReadOnlyCollection<string>? ErrorMessages { get; init; }

    public static ProblemDetailsModel Create(string errorCode, string errorMessage)
    {
        return new ProblemDetailsModel()
        {
            ErrorCode = errorCode,
            ErrorMessages = [errorMessage]
        };
    }
}

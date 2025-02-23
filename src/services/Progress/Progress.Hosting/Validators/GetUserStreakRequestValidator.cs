using Egeshka.Core.Models.Constants;
using Egeshka.Grpc.Progress;
using FluentValidation;

namespace Egeshka.Progress.Hosting.Validators;

public sealed class GetUserStreakRequestValidator : AbstractValidator<GetUserStreak.Types.Request>
{
    public GetUserStreakRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);
    }
}

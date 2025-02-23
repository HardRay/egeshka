using Egeshka.Core.Models.Constants;
using Egeshka.Grpc.Progress;
using FluentValidation;

namespace Egeshka.Progress.Hosting.Validators;

public sealed class GetCompletedExercisesRequestValidator : AbstractValidator<GetCompletedExercises.Types.Request>
{
    public GetCompletedExercisesRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);

        RuleFor(x => x.SubjectId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);
    }
}
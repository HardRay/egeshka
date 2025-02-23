using Egeshka.Core.Models.Constants;
using Egeshka.Grpc.Progress;
using FluentValidation;

namespace Egeshka.Progress.Hosting.Validators;

public sealed class SaveExerciseResultRequestValidator : AbstractValidator<SaveExerciseResult.Types.Request>
{
    public SaveExerciseResultRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);

        RuleFor(x => x.SubjectId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);

        RuleFor(x => x.ExerciseId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);

        RuleForEach(x => x.ErrorTaskIds)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);

        RuleFor(x => x.ExperiencePoints)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.FieldMustBePositive);
    }
}

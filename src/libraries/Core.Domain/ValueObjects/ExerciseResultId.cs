namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct ExerciseResultId(long Value)
{
    public static implicit operator long(ExerciseResultId exerciseResultId) => exerciseResultId.Value;
}


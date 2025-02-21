namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct ExerciseId(long Value)
{
    public static implicit operator long(ExerciseId exerciseId) => exerciseId.Value;
}


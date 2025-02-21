namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct SubjectId(long Value)
{
    public static implicit operator long(SubjectId subjectId) => subjectId.Value;
}


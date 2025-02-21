namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct TaskId(long Value)
{
    public static implicit operator long(TaskId taskId) => taskId.Value;
}


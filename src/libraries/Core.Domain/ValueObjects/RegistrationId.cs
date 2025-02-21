namespace Egeshka.Core.Domain.ValueObjects;

public readonly record struct RegistrationId(long Value)
{
    public static implicit operator long(RegistrationId registrationId) => registrationId.Value;
}
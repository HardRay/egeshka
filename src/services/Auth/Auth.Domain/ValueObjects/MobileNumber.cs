using Egeshka.Auth.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Egeshka.Auth.Domain.ValueObjects;

public sealed partial class MobileNumber
{
    public string Value { get; }

    private MobileNumber(string value) => Value = value;

    public static MobileNumber Create(string value)
    {
        var isMatch = IncorrectSymbols().IsMatch(value);

        if (!isMatch)
        {
            throw new DomainException(code: 1, $"Номер телефона `{value}` содержит недопустимые символы");
        }

        return new MobileNumber(value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is MobileNumber email)
            return Equals(email);

        return false;
    }

    private bool Equals(MobileNumber other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static implicit operator string(MobileNumber mobileNumber) => mobileNumber.Value;

    [GeneratedRegex("^[\\s\\d\\(\\)\\-\\+]+$")]
    private static partial Regex IncorrectSymbols();
}
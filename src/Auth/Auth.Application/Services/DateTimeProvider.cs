using Egeshka.Auth.Application.Services.Interfaces;

namespace Egeshka.Auth.Application.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;

    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

using Egeshka.Core.Application.Services.Interfaces;

namespace Egeshka.Core.Application.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;

    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

namespace Egeshka.Core.Application.Services.Interfaces;

public interface IDateTimeProvider
{
    public DateTimeOffset Now { get; }
    public DateTimeOffset UtcNow { get; }
}

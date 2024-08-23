using Application.Abstractions.Clock;

namespace Infrastructure.Clock;

public sealed class DateTimeProvider  : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
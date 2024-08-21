﻿using Application.Abstractions;

namespace Infrastructure.Clock;

public sealed class DateTimeProvider  : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
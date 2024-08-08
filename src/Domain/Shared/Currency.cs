// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

namespace Domain.Shared;

public record Currency
{
    internal static Currency NONE = new("");
    public static Currency EUR = new("EUR");
    public static Currency USD = new("USD");
    public static Currency GBP = new("GBP");

    public static implicit operator Currency(string code) => new(code);
    private Currency(string code) => Code = code;
    public string Code { get; init; }

    public static Currency FromCode(string code) => All.FirstOrDefault(x => x.Code == code) ??
                                                    throw new ApplicationException("Invalid currency code");

    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        EUR, USD, GBP
    };
}
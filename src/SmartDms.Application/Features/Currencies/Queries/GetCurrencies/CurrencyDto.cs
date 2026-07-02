namespace JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;

public record CurrencyDto(
    int Id,
    string CurrencyCode,
    string CurrencyName,
    decimal BuyRate,
    decimal SellRate,
    bool IsBase,
    bool Active,
    string? CurrencyNo,
    int? SupplierId,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

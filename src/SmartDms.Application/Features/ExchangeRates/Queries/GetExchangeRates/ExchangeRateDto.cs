namespace JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;

public record ExchangeRateDto(
    int Id,
    string CurrencyCode,
    decimal Rate,
    decimal Ask,
    decimal Bid,
    decimal Average,
    DateTime RateDate,
    string? Note,
    DateTime CreatedDate,
    string CreatedBy);

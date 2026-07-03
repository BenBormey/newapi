using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;

public record GetExchangeRatesQuery(
    string? CurrencyCode,
    DateTime? FromDate,
    DateTime? ToDate,
    int Page = 1,
    int PageSize = 20) : IQuery<PagedResult<ExchangeRateDto>>;

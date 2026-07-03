using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;

public sealed class GetExchangeRatesHandler
    : IQueryHandler<GetExchangeRatesQuery, PagedResult<ExchangeRateDto>>
{
    private readonly IExchangeRateRepository _repository;

    public GetExchangeRatesHandler(IExchangeRateRepository repository) => _repository = repository;

    public Task<PagedResult<ExchangeRateDto>> Handle(GetExchangeRatesQuery q, CancellationToken ct)
    {
        var page = Math.Max(1, q.Page);
        var pageSize = Math.Clamp(q.PageSize, 1, 100);

        return _repository.GetExchangeRatesAsync(
            q.CurrencyCode?.Trim().ToUpperInvariant(),
            q.FromDate, q.ToDate, page, pageSize, ct);
    }
}

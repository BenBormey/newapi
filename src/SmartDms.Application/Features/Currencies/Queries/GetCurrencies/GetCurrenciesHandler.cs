
using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;

public sealed class GetCurrenciesHandler
    : IQueryHandler<GetCurrenciesQuery, PagedResult<CurrencyDto>>
{
    private readonly ICurrencyRepository _repository;

    public GetCurrenciesHandler(ICurrencyRepository repository) => _repository = repository;

    public Task<PagedResult<CurrencyDto>> Handle(GetCurrenciesQuery q, CancellationToken ct)
    {
        var page = Math.Max(1, q.Page);
        var pageSize = Math.Clamp(q.PageSize, 1, 100);
        return _repository.GetCurrenciesAsync(q.Search, q.Active, page, pageSize, ct);
    }
}

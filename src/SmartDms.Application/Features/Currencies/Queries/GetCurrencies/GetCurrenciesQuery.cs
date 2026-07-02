using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;

public record GetCurrenciesQuery(
    string? Search,          // រក code ឬ name
    bool? Active,            // filter active
    int Page = 1,
    int PageSize = 20) : IQuery<PagedResult<CurrencyDto>>;

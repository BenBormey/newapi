using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.ExchangeRates.Queries.GetLatestRate;

public sealed class GetLatestRateHandler : IQueryHandler<GetLatestRateQuery, ExchangeRateDto?>
{
    private readonly IExchangeRateRepository _repository;

    public GetLatestRateHandler(IExchangeRateRepository repository) => _repository = repository;

    public Task<ExchangeRateDto?> Handle(GetLatestRateQuery q, CancellationToken ct)
        => _repository.GetLatestAsync(q.CurrencyCode.Trim().ToUpperInvariant(), ct);
}

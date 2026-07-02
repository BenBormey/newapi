
using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Currencies.Queries.GetCurrencyById;

public sealed class GetCurrencyByIdHandler : IQueryHandler<GetCurrencyByIdQuery, CurrencyDto?>
{
    private readonly ICurrencyRepository _repository;

    public GetCurrencyByIdHandler(ICurrencyRepository repository) => _repository = repository;

    public Task<CurrencyDto?> Handle(GetCurrencyByIdQuery q, CancellationToken ct)
        => _repository.GetByIdAsync(q.Id, ct);
}

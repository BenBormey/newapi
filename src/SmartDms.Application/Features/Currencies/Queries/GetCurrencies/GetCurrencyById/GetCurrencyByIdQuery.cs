
using JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Currencies.Queries.GetCurrencyById;

public record GetCurrencyByIdQuery(int Id) : IQuery<CurrencyDto?>;

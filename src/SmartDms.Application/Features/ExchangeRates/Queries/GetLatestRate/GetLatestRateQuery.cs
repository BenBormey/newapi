using JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.ExchangeRates.Queries.GetLatestRate;


public record GetLatestRateQuery(string CurrencyCode) : IQuery<ExchangeRateDto?>;

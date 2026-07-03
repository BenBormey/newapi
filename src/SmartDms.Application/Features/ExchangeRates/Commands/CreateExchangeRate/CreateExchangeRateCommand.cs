using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Commands.CreateExchangeRate;

public record CreateExchangeRateCommand(
    string CurrencyCode,
    decimal Rate,
    decimal Ask,
    decimal Bid,
    decimal? Average,      // null = database គណនា (Ask+Bid)/2 ឱ្យ
    DateTime RateDate,
    string? Note,
    string CreatedBy) : ICommand<Result<int>>;

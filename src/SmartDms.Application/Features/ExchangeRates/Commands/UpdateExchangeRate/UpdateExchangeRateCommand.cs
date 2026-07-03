using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Commands.UpdateExchangeRate;

public record UpdateExchangeRateCommand(
    int Id,
    decimal Rate,
    decimal Ask,
    decimal Bid,
    decimal? Average,
    DateTime RateDate,
    string? Note) : ICommand<Result>;

using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Commands.UpdateExchangeRate;

public sealed class UpdateExchangeRateHandler
    : ICommandHandler<UpdateExchangeRateCommand, Result>
{
    private readonly IExchangeRateRepository _repository;

    public UpdateExchangeRateHandler(IExchangeRateRepository repository) => _repository = repository;

    public async Task<Result> Handle(UpdateExchangeRateCommand cmd, CancellationToken ct)
    {
        if (cmd.Rate <= 0 || cmd.Ask <= 0 || cmd.Bid <= 0)
            return Result.Failure("Rate, Ask, Bid must be greater than 0");

        if (cmd.RateDate > DateTime.Today)
            return Result.Failure("RateDate cannot be in the future");

        var rows = await _repository.UpdateAsync(
            cmd.Id, cmd.Rate, cmd.Ask, cmd.Bid, cmd.Average,
            cmd.RateDate.Date, cmd.Note?.Trim(), ct);

        return rows > 0
            ? Result.Success()
            : Result.Failure($"ExchangeRate with id {cmd.Id} not found");
    }
}

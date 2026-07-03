using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Commands.CreateExchangeRate;

public sealed class CreateExchangeRateHandler
    : ICommandHandler<CreateExchangeRateCommand, Result<int>>
{
    private readonly IExchangeRateRepository _repository;
    private readonly ICurrencyRepository _currencies;

    public CreateExchangeRateHandler(
        IExchangeRateRepository repository,
        ICurrencyRepository currencies)
    {
        _repository = repository;
        _currencies = currencies;
    }

    public async Task<Result<int>> Handle(CreateExchangeRateCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.CurrencyCode))
            return Result.Failure<int>("CurrencyCode is required");

        if (cmd.Rate <= 0 || cmd.Ask <= 0 || cmd.Bid <= 0)
            return Result.Failure<int>("Rate, Ask, Bid must be greater than 0");

        if (cmd.RateDate > DateTime.Today)
            return Result.Failure<int>("RateDate cannot be in the future");

        if (string.IsNullOrWhiteSpace(cmd.CreatedBy))
            return Result.Failure<int>("CreatedBy is required");

        var code = cmd.CurrencyCode.Trim().ToUpperInvariant();

        // ពិនិត្យ currency ត្រូវមានក្នុង table Currency សិន (cross-feature validation!)
        var currency = await _currencies.GetByCodeAsync(code, ct);
        if (currency is null)
            return Result.Failure<int>($"Currency '{code}' does not exist. Create it first.");

        var id = await _repository.InsertAsync(
            code, cmd.Rate, cmd.Ask, cmd.Bid, cmd.Average,
            cmd.RateDate.Date, cmd.Note?.Trim(), cmd.CreatedBy.Trim(), ct);

        return Result.Success(id);
    }
}

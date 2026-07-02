
using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Commands.UpdateCurrency;

public sealed class UpdateCurrencyHandler : ICommandHandler<UpdateCurrencyCommand, Result>
{
    private readonly ICurrencyRepository _repository;

    public UpdateCurrencyHandler(ICurrencyRepository repository) => _repository = repository;

    public async Task<Result> Handle(UpdateCurrencyCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.CurrencyName))
            return Result.Failure("CurrencyName is required");

        if (cmd.BuyRate <= 0 || cmd.SellRate <= 0)
            return Result.Failure("BuyRate and SellRate must be greater than 0");

        var rows = await _repository.UpdateAsync(
            cmd.Id, cmd.CurrencyName.Trim(), cmd.BuyRate, cmd.SellRate,
            cmd.IsBase, cmd.Active, cmd.CurrencyNo?.Trim(), cmd.SupplierId, ct);

        return rows > 0
            ? Result.Success()
            : Result.Failure($"Currency with id {cmd.Id} not found");
    }
}

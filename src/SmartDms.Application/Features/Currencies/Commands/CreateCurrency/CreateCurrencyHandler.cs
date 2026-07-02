
using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Commands.CreateCurrency;

public sealed class CreateCurrencyHandler : ICommandHandler<CreateCurrencyCommand, Result<int>>
{
    private readonly ICurrencyRepository _repository;

    public CreateCurrencyHandler(ICurrencyRepository repository) => _repository = repository;

    public async Task<Result<int>> Handle(CreateCurrencyCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.CurrencyCode) || cmd.CurrencyCode.Trim().Length != 3)
            return Result.Failure<int>("CurrencyCode must be exactly 3 characters (e.g. USD, KHR)");

        if (string.IsNullOrWhiteSpace(cmd.CurrencyName))
            return Result.Failure<int>("CurrencyName is required");

        if (cmd.BuyRate <= 0 || cmd.SellRate <= 0)
            return Result.Failure<int>("BuyRate and SellRate must be greater than 0");

        var code = cmd.CurrencyCode.Trim().ToUpperInvariant();

        // ពិនិត្យ code ជាន់គ្នា
        var existing = await _repository.GetByCodeAsync(code, ct);
        if (existing is not null)
            return Result.Failure<int>($"Currency code '{code}' already exists");

        var id = await _repository.InsertAsync(
            code, cmd.CurrencyName.Trim(), cmd.BuyRate, cmd.SellRate,
            cmd.IsBase, cmd.CurrencyNo?.Trim(), cmd.SupplierId, ct);

        return Result.Success(id);
    }
}

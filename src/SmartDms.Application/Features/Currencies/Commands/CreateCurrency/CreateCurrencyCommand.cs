
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Commands.CreateCurrency;

public record CreateCurrencyCommand(
    string CurrencyCode,
    string CurrencyName,
    decimal BuyRate,
    decimal SellRate,
    bool IsBase,
    string? CurrencyNo,
    int? SupplierId) : ICommand<Result<int>>;


using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Commands.UpdateCurrency;

public record UpdateCurrencyCommand(
    int Id,
    string CurrencyName,
    decimal BuyRate,
    decimal SellRate,
    bool IsBase,
    bool Active,
    string? CurrencyNo,
    int? SupplierId) : ICommand<Result>;

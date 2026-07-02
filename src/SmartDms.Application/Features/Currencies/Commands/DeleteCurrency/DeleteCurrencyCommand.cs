
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Commands.DeleteCurrency;

/// <summary>Soft delete: set Active = 0 (មិនលុបចេញពី database ពិត - professional!)</summary>
public record DeleteCurrencyCommand(int Id) : ICommand<Result>;

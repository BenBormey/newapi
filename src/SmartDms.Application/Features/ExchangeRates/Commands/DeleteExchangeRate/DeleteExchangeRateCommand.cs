using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Commands.DeleteExchangeRate;

/// <summary>Hard delete (table គ្មាន IsActive column)</summary>
public record DeleteExchangeRateCommand(int Id) : ICommand<Result>;

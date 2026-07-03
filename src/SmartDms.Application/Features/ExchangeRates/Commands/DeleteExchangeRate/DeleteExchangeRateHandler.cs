using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.ExchangeRates.Commands.DeleteExchangeRate;

public sealed class DeleteExchangeRateHandler
    : ICommandHandler<DeleteExchangeRateCommand, Result>
{
    private readonly IExchangeRateRepository _repository;

    public DeleteExchangeRateHandler(IExchangeRateRepository repository) => _repository = repository;

    public async Task<Result> Handle(DeleteExchangeRateCommand cmd, CancellationToken ct)
    {
        var rows = await _repository.DeleteAsync(cmd.Id, ct);
        return rows > 0
            ? Result.Success()
            : Result.Failure($"ExchangeRate with id {cmd.Id} not found");
    }
}

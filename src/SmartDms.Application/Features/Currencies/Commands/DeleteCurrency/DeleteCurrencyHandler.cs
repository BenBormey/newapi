
using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Currencies.Commands.DeleteCurrency;

public sealed class DeleteCurrencyHandler : ICommandHandler<DeleteCurrencyCommand, Result>
{
    private readonly ICurrencyRepository _repository;

    public DeleteCurrencyHandler(ICurrencyRepository repository) => _repository = repository;

    public async Task<Result> Handle(DeleteCurrencyCommand cmd, CancellationToken ct)
    {
        var rows = await _repository.SoftDeleteAsync(cmd.Id, ct);
        return rows > 0
            ? Result.Success()
            : Result.Failure($"Currency with id {cmd.Id} not found");
    }
}

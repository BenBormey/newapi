using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Commands.DeleteUom;

public sealed class DeleteUomHandler : ICommandHandler<DeleteUomCommand, Result>
{
    private readonly IUomRepository _repository;

    public DeleteUomHandler(IUomRepository repository) => _repository = repository;

    public async Task<Result> Handle(DeleteUomCommand cmd, CancellationToken ct)
    {
        var rows = await _repository.SoftDeleteAsync(cmd.Id, ct);
        return rows > 0
            ? Result.Success()
            : Result.Failure($"UOM with id {cmd.Id} not found");
    }
}

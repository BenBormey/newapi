using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Commands.UpdateUom;

public sealed class UpdateUomHandler : ICommandHandler<UpdateUomCommand, Result>
{
    private readonly IUomRepository _repository;

    public UpdateUomHandler(IUomRepository repository) => _repository = repository;

    public async Task<Result> Handle(UpdateUomCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.UOMCode))
            return Result.Failure("UOMCode is required");

        if (string.IsNullOrWhiteSpace(cmd.UOMName))
            return Result.Failure("UOMName is required");

        var rows = await _repository.UpdateAsync(
            cmd.Id, cmd.UOMCode.Trim().ToUpperInvariant(), cmd.UOMName.Trim(), cmd.IsActive, ct);

        return rows > 0
            ? Result.Success()
            : Result.Failure($"UOM with id {cmd.Id} not found");
    }
}

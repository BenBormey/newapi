using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Commands.CreateUom;

public sealed class CreateUomHandler : ICommandHandler<CreateUomCommand, Result<int>>
{
    private readonly IUomRepository _repository;

    public CreateUomHandler(IUomRepository repository) => _repository = repository;

    public async Task<Result<int>> Handle(CreateUomCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.UOMCode))
            return Result.Failure<int>("UOMCode is required");

        if (cmd.UOMCode.Trim().Length > 50)
            return Result.Failure<int>("UOMCode must not exceed 50 characters");

        if (string.IsNullOrWhiteSpace(cmd.UOMName))
            return Result.Failure<int>("UOMName is required");

        var code = cmd.UOMCode.Trim().ToUpperInvariant();

        var existing = await _repository.GetByCodeAsync(code, ct);
        if (existing is not null)
            return Result.Failure<int>($"UOM code '{code}' already exists");

        var id = await _repository.InsertAsync(code, cmd.UOMName.Trim(), ct);
        return Result.Success(id);
    }
}

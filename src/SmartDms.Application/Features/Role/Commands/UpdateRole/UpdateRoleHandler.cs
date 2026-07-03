using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Commands.UpdateRole;

public sealed class UpdateRoleHandler : ICommandHandler<UpdateRoleCommand, Result>
{
    private readonly IRoleRepository _repository;

    public UpdateRoleHandler(IRoleRepository repository) => _repository = repository;

    public async Task<Result> Handle(UpdateRoleCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.RoleName))
            return Result.Failure("RoleName is required");

        var existing = await _repository.GetByIdAsync(cmd.Id, ct);
        if (existing is null)
            return Result.Failure($"Role with id {cmd.Id} not found");

        if (existing.IsSystemRole)
            return Result.Failure("System roles cannot be modified");

        var rows = await _repository.UpdateAsync(
            cmd.Id, cmd.RoleName.Trim(), cmd.Description?.Trim(), cmd.IsActive, ct);

        return rows > 0
            ? Result.Success()
            : Result.Failure($"Role with id {cmd.Id} not found");
    }
}

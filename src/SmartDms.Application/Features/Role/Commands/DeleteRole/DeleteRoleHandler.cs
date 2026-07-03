using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Commands.DeleteRole;

public sealed class DeleteRoleHandler : ICommandHandler<DeleteRoleCommand, Result>
{
    private readonly IRoleRepository _repository;

    public DeleteRoleHandler(IRoleRepository repository) => _repository = repository;

    public async Task<Result> Handle(DeleteRoleCommand cmd, CancellationToken ct)
    {
        // ការពារ system role កុំឱ្យលុប
        var existing = await _repository.GetByIdAsync(cmd.Id, ct);
        if (existing is null)
            return Result.Failure($"Role with id {cmd.Id} not found");

        if (existing.IsSystemRole)
            return Result.Failure("System roles cannot be deleted");

        var rows = await _repository.SoftDeleteAsync(cmd.Id, ct);
        return rows > 0
            ? Result.Success()
            : Result.Failure($"Role with id {cmd.Id} not found");
    }
}

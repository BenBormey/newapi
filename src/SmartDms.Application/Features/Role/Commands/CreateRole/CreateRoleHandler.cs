using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Commands.CreateRole;

public sealed class CreateRoleHandler : ICommandHandler<CreateRoleCommand, Result<int>>
{
    private readonly IRoleRepository _repository;

    public CreateRoleHandler(IRoleRepository repository) => _repository = repository;

    public async Task<Result<int>> Handle(CreateRoleCommand cmd, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(cmd.RoleCode))
            return Result.Failure<int>("RoleCode is required");

        if (cmd.RoleCode.Trim().Length > 50)
            return Result.Failure<int>("RoleCode must not exceed 50 characters");

        if (string.IsNullOrWhiteSpace(cmd.RoleName))
            return Result.Failure<int>("RoleName is required");

        var code = cmd.RoleCode.Trim().ToUpperInvariant();

        var existing = await _repository.GetByCodeAsync(code, ct);
        if (existing is not null)
            return Result.Failure<int>($"Role code '{code}' already exists");

        var id = await _repository.InsertAsync(code, cmd.RoleName.Trim(), cmd.Description?.Trim(), ct);
        return Result.Success(id);
    }
}

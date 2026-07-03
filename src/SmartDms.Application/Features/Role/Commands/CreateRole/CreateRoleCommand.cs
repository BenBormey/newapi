using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(string RoleCode, string RoleName, string? Description)
    : ICommand<Result<int>>;

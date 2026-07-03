using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(
    int Id,
    string RoleName,
    string? Description,
    bool IsActive) : ICommand<Result>;

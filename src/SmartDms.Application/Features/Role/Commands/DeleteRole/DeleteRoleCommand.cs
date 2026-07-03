using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Commands.DeleteRole;

/// <summary>Soft delete: IsActive = 0 (ការពារ system role)</summary>
public record DeleteRoleCommand(int Id) : ICommand<Result>;

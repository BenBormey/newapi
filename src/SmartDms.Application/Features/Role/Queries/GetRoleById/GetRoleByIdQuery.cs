using JuJuBi.Application.Features.Roles.Queries.GetRoles;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(int Id) : IQuery<RoleDto?>;

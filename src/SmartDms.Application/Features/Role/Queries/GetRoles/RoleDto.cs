namespace JuJuBi.Application.Features.Roles.Queries.GetRoles;

public record RoleDto(
    int Id,
    string RoleCode,
    string RoleName,
    string? Description,
    bool IsSystemRole,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

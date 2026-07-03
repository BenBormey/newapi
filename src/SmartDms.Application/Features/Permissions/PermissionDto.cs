namespace JuJuBi.Application.Features.Permissions;

public record PermissionDto(
    int Id,
    string PermissionCode,
    string PermissionName,
    string Module,
    bool IsActive);

using Microsoft.AspNetCore.Authorization;

namespace JuJuBi.Api.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        => Policy = $"permission:{permission}";
}

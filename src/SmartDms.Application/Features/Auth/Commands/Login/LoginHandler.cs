using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Auth;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.Auth.Commands.Login;

public sealed class LoginHandler : ICommandHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwt;
    private readonly IPermissionRepository _permissions;   // ← ថ្មី

    public LoginHandler(
        IUserRepository users,
        IPasswordHasher hasher,
        IJwtTokenGenerator jwt,
        IPermissionRepository permissions)                 // ← ថ្មី
    {
        _users = users;
        _hasher = hasher;
        _jwt = jwt;
        _permissions = permissions;                        // ← ថ្មី
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand cmd, CancellationToken ct)
    {
        var user = await _users.GetByUsernameAsync(cmd.Username?.Trim() ?? "", ct);

        if (user is null)
            return Result.Failure<LoginResponse>("Invalid username or password");

        bool isValid = false;

        try
        {
            isValid = _hasher.Verify(cmd.Password ?? "", user.PasswordHash);
        }
        catch
        {
            return Result.Failure<LoginResponse>("Invalid username or password");
        }

        if (!isValid)
            return Result.Failure<LoginResponse>("Invalid username or password");

        // អាន permissions របស់ user ដាក់ចូល token
        var permissions = await _permissions.GetUserPermissionCodesAsync(user.Id, ct);

        var token = _jwt.Generate(user, permissions);      // ← បន្ថែម permissions

        return Result.Success(new LoginResponse(
            token,
            user.Id,
            user.Username,
            user.FullName,
            user.RoleId,
            user.RoleCode,
            user.RoleName
        ));
    }
}

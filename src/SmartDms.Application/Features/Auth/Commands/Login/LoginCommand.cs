using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Username,
    string Password
) : ICommand<Result<LoginResponse>>;

public record LoginResponse(
    string Token,
    int UserId,
    string Username,
    string FullName,
    int RoleId,
    string RoleCode,
    string RoleName
);

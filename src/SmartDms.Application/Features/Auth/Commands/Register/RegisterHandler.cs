using JuJuBis.Application.Abstractions.Auth;
using JuJuBis.Application.Abstractions.Auth.Commands.Register;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.Auth.Commands.Register;

public sealed class RegisterHandler
    : ICommandHandler<RegisterCommand, Result<int>>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterHandler(
        IUserRepository users,
        IPasswordHasher passwordHasher)
    {
        _users = users;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<int>> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(command.Username))
            return Result.Failure<int>("Username is required.");

        if (command.Username.Length < 3)
            return Result.Failure<int>("Username must be at least 3 characters.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return Result.Failure<int>("Password is required.");

        if (command.Password.Length < 6)
            return Result.Failure<int>("Password must be at least 6 characters.");

        if (string.IsNullOrWhiteSpace(command.Email))
            return Result.Failure<int>("Email is required.");

        // Check duplicate username
        var existingUser = await _users.GetByUsernameAsync(
            command.Username.Trim(),
            cancellationToken);

        if (existingUser != null)
            return Result.Failure<int>("Username already exists.");

        var user = new User
        {
            Username = command.Username.Trim(),
            PasswordHash = _passwordHasher.Hash(command.Password),
            FullName = command.FullName,
            FullNameKh = command.FullNameKh,
            Email = command.Email.Trim(),
            Phone = command.Phone,
            Address = command.Address,
            AddressKh = command.AddressKh,
            RoleId = command.RoleId,
            OutletId = command.OutletId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var id = await _users.InsertAsync(user, cancellationToken);

        return Result.Success(id);
    }
}

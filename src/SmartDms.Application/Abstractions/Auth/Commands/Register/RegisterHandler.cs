using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Abstractions.Auth.Commands.Register
{
    public sealed class RegisterHandler : ICommandHandler<RegisterCommand, Result<int>>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        public RegisterHandler(IUserRepository user, IPasswordHasher pass)
        {
            _users = user;
            _hasher = pass;

        }


        public async Task<Result<int>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Username) || command.Username.Length < 3)
                return Result.Failure<int>("Username must be at least 3 characters");

            if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < 6)
                return Result.Failure<int>("Password must be at least 6 characters");

            if (string.IsNullOrWhiteSpace(command.Email) || !command.Email.Contains('@'))
                return Result.Failure<int>("Invalid email");

            // Check duplicate username
            var existing = await _users.GetByUsernameAsync(command.Username.Trim(), cancellationToken);

            if (existing is not null)
                return Result.Failure<int>("Username already exists");

            var user = new User
            {
                Username = command.Username.Trim(),
                PasswordHash = _hasher.Hash(command.Password),
                FullName = command.FullName,
                FullNameKh = command.FullNameKh,
                Email = command.Email,
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Abstractions.Auth.Commands.Register
{
    public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string FullName,
    string? FullNameKh,
    string? Phone,
    string? Address,
    string? AddressKh,
    int? OutletId,
    int RoleId
) : ICommand<Result<int>>;
}

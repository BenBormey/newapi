using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuJuBis.Domain.Entities;

namespace JuJuBis.Application.Abstractions.Data
{
    public   interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken ct);
        Task<int> InsertAsync(User user, CancellationToken ct);
    }
}

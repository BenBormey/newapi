using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuJuBis.Domain.Entities;

namespace JuJuBis.Application.Abstractions.Auth
{
    public interface IJwtTokenGenerator
    {
        string Generate(User user, IEnumerable<string> permissions);
    }
}

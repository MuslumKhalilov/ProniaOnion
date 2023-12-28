using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Tokens;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateJwt(AppUser user, int minutes);
    }
}

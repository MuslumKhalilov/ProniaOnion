using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Account;
using ProniaOnion.Application.Dtos.Tokens;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task Register(RegisterDto dto);
        Task<TokenResponseDto> Login(LoginDto dto);
    }
}

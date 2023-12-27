using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Dtos.Account
{
    public record LoginDto(string UsernameOrEmail,string Password);
    
}

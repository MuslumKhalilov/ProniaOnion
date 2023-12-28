using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Dtos.Tokens
{
    public record TokenResponseDto(string Token,string Username, DateTime ExpireTime);
    
    
}

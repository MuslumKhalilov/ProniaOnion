using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Dtos.Account
{
    public record RegisterDto(string Name,string Surname,string Username,string Email, string Password,string ConfirmPassword);
    
}

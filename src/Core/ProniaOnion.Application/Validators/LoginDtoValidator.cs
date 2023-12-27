using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.Dtos.Account;

namespace ProniaOnion.Application.Validators
{
    internal class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x=>x.UsernameOrEmail).NotEmpty().WithMessage("Username or Email required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password required").MinimumLength(8);
        }
    }
}

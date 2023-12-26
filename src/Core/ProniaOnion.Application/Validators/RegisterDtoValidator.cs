using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.Dtos.Account;

namespace ProniaOnion.Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Username).NotEmpty().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().MinimumLength(4).MaximumLength(256);
            RuleFor(x => x).Must(x => x.Password == x.ConfirmPassword);
        }
    }
}

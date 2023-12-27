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
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name Required").MinimumLength(3).WithMessage("Name should contain minimum 3 characters").MaximumLength(50).WithMessage("Name should contain maximum 50 characters");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname Required").MinimumLength(3).WithMessage("Surname should contain minimum 3 characters").MaximumLength(50).WithMessage("Surname should contain maximum 50 characters");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username Required").MinimumLength(4).WithMessage("Username should contain minimum 4 characters").MaximumLength(50).WithMessage("Username should contain maximum 50 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Required").MinimumLength(4).WithMessage("Email should contain minimum 4 characters").MaximumLength(256).EmailAddress().WithMessage("Email should contain maximum 256 characters");
            RuleFor(x => x).Must(x => x.Password == x.ConfirmPassword).WithMessage("Incorrect Password. Try again");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Validators
{
    public class TagCreateDtoValidator:AbstractValidator<Tag>
    {
        public TagCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1); 
        }
    }
}

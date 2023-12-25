using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.Dtos.Products;

namespace ProniaOnion.Application.Validators
{
    public class ProductCreateValidator:AbstractValidator<ProductCreatedDto>
    {
      
        public ProductCreateValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0).WithMessage("Id should be greater than 0");
            RuleForEach(x => x.ColorIds).GreaterThan(0).WithMessage("Id should be greater than 0");
        }
    }
}

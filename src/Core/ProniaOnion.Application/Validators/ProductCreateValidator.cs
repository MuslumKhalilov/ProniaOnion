using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProniaOnion.Application.Dtos.Products;

namespace ProniaOnion.Application.Validators
{
    internal class ProductCreateValidator:AbstractValidator<ProductCreatedDto>
    {
      
        public ProductCreateValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0).WithMessage("Id 0 dan boyuk olmalidir");
        }
    }
}

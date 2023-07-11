using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategory>
    {
        public CreateProductCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().NotNull().WithMessage("The {PropertyName} cant be empty");
        }
    }
}

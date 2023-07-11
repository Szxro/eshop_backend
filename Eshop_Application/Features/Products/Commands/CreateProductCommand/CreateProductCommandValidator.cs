using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ProductName).NotNull().WithMessage("The {PropertyName} cant be empty");

            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("The {PropertyName} need to be greater than 0");

            RuleFor(x => x.ProductQuantity).GreaterThan(0).WithMessage("The {PropertyName} need to be greater than 0");

            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("The {PropertyName} cant be empty");

            RuleForEach(x => x.ProductCategory).SetValidator(new CreateProductCategoryCommandValidator());
        }
    }
}

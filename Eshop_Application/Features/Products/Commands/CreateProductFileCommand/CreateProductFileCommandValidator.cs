using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Commands.CreateProductFileCommand
{
    public class CreateProductFileCommandValidator: AbstractValidator<CreateProductFileCommand>
    {
        public CreateProductFileCommandValidator()
        {
            RuleFor(x => x.productName).NotEmpty().NotNull().WithMessage("The {PropertyName} cant be null");
        }
    }
}

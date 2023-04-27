using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.User.Commands.RegisterUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //In the first failure it stop and continue the next validation
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The {PropertyName} cant be empty");
        }
    }
}

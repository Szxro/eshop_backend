using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Users.Commands.LoginUserCommand
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Username).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The {PropertyName} cant be empty");
        }
    }
}

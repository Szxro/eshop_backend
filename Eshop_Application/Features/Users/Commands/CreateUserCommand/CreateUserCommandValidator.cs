using Eshop_Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Users.Commands.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //In the first failure it stop and continue the next validation
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("The {PropertyName} cant be empty");

            RuleFor(x => x.Email).EmailAddress().WithMessage("The {PropertyName} need to be a valid email");

            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("The {PropertyName} cant be empty");
        }
    }
}

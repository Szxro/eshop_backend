using Eshop_Application.Features.Users.Commands.GetCurrentUserByUsernameCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Users.Commands.GetCurrentUserByUsernameCommand
{
    public class GetCurrentUserByUsernameCommandValidator : AbstractValidator<GetCurrentUserByUsername>
    {
        public GetCurrentUserByUsernameCommandValidator()
        {
            RuleFor(x => x.username).NotEmpty().WithMessage("The {PropertyName} cant be empty");
        }
    }
}

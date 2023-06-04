using Eshop_Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.User.Commands.RegisterUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IValidatorInput _validator;

        public CreateUserCommandValidator(IValidatorInput validator)
        {
            //DI
            _validator = validator;

            //In the first failure it stop and continue the next validation
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            //One way
            RuleFor(x => x.Email).MustAsync(async(email,cancellation) => 
            {
                bool IsEmailExists = await _validator.CheckEmailAlreadyExists(email, cancellation);

                return !IsEmailExists; //(need to be false to trigger the ValidationException)
            }).WithMessage("The email already exists");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("The {PropertyName} cant be empty");
            //Other Way
            RuleFor(x => x.UserName).MustAsync(_validator.CheckUsernameAlreadyExists).WithMessage("The username already exists");
        }
    }
}

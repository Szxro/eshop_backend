using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Mapping;
using Eshop_Domain.Entities.UserEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Users.Commands.CreateUserCommand;

public record CreateUserCommand : IRequest<Unit>
{
    public CreateUserCommand()
    {
        UserShippingInfo = new HashSet<CreateUserShippingInfo>();   
    }

    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string ConfirmPassword { get; init; } = string.Empty;

    public ICollection<CreateUserShippingInfo> UserShippingInfo { get; set; }
}

public record CreateUserShippingInfo
{
    public string Address { get; init; } = string.Empty;

    public string Country { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string State { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public string PostalCode { get; init; } = string.Empty;

    public string CountryCode { get; init; } = string.Empty;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IUserRepository _user;
    private readonly IPasswordUtilities _password;
    private readonly IRoleRepository _role;

    public CreateUserCommandHandler(
        IUserRepository user, 
        IPasswordUtilities password,
        IRoleRepository role)
    {
        _user = user;
        _password = password;
        _role = role;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //Checking the equality of the password
        _password.verifyPasswordsEquality(request.Password, request.ConfirmPassword);

        //Getting the UserRole
        UserRoles? customerRole = await _role.GetRoleByName("Customer",cancellationToken);

        if (customerRole is null)
        {
            throw new NotFoundException("The role was not found");
        }

        //Generating the UserHash and Salt
        string userHash = _password.GenerateUserHashAndSalt(request.Password, out byte[] userSalt);

        //Mapping the user
        User newUser = request.ToUser(userSalt,customerRole);

        //Creating the user
        await _user.CreateUser(newUser, userHash,cancellationToken);

        return Unit.Value;
    }
}


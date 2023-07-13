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
using System.Transactions;

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
    private readonly IPasswordRepository _password;
    private readonly IRoleRepository _role;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository user, 
        IPasswordRepository password,
        IRoleRepository role,
        IUnitOfWork unitOfWork)
    {
        _user = user;
        _password = password;
        _role = role;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //Checking the equality of the password
        if (!CheckUserPassword(request.Password, request.ConfirmPassword)) throw new PasswordException("The password and confirm password field have to be the same");

        //Getting the UserRole
        UserRoles? customerRole = await _role.GetRoleByName("Customer", cancellationToken);

        if (customerRole is null)
        {
            throw new NotFoundException("The role was not found");
        }

        //Generating the UserHash and Salt
        string userHash = _password.GenerateUserHashAndSalt(request.Password, out byte[] userSalt);

        //Mapping the user
        User newUser = request.ToUser(userSalt, customerRole);

        //Changing the state of the user roles 
        newUser.UserUserRoles.Select(x => _unitOfWork.ChangeEntityStateToUnChanged(x.UserRoles)).ToList();

        //Creating the user
        _user.CreateUser(newUser, userHash, cancellationToken);

        //Using the Unit of work
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private bool CheckUserPassword(string password,string confirmPassword) => password.Equals(confirmPassword);
}


using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.UserEntities;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Users.Commands.GetCurrentUserByUsernameCommand;

public record GetCurrentUserByUsername(string username):IRequest<CurrentUserDTO?>;

public class GetCurrentUserByUsernameHandler : IRequestHandler<GetCurrentUserByUsername, CurrentUserDTO?>
{
    private readonly IUserRepository _user;

    public GetCurrentUserByUsernameHandler(IUserRepository user)
    {
        _user = user;
    }
    public async Task<CurrentUserDTO?> Handle(GetCurrentUserByUsername request, CancellationToken cancellationToken)
    {
        User? currentUser = await _user.GetUserByUsername(request.username);

        if (currentUser is null) throw new NotFoundException("The user was not found");

        return new CurrentUserDTO() { UserName = currentUser.UserName, Email = currentUser.Email };
    }
}

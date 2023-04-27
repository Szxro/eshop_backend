using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.TokenEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.User.Commands.LoginUserCommand;

public record LoginUserCommand(string Email,string Password) : IRequest<TokenResponse>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
{
    private readonly IAuthRepository _auth;

    public LoginUserCommandHandler(IAuthRepository auth)
    {
      _auth = auth;
    }
    public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _auth.LoginUserAsync(request);
    }
}


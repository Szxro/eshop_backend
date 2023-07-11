using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.TokenEntities;
using Eshop_Domain.Entities.UserEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Eshop_Application.Features.Users.Commands.LoginUserCommand;

public record LoginUserCommand(string Username,string Password) : IRequest<TokenResponse>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
{
    private readonly IUserRepository _user;
    private readonly IPasswordRepository _password;
    private readonly ITokenRepository _token;

    public LoginUserCommandHandler(
        IUserRepository user,
        IPasswordRepository password,
        ITokenRepository token)
    {
      _user = user;
      _password = password;
      _token = token;
    }

    public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User? currentUser = await _user.GetUserByUsername(request.Username);

        if (currentUser is null) throw new NotFoundException($"The user {request.Username} was not found");

        string? userHash = _user.GetUserHash(currentUser);

        if (string.IsNullOrWhiteSpace(userHash)) throw new ArgumentException("Invalid UserHash");

        byte[]? userSalt = _user.GetUserSalt(currentUser);

        if (userSalt is null) throw new ArgumentException("Invalid userHash");

        bool isPasswodCorrect = _password.VerifyPasswordHash(request.Password,userHash,userSalt);

        if (!isPasswodCorrect) throw new PasswordException("The password is incorrect");

        //Generating the token and refresh token

        TokenResponse response = new TokenResponse()
        {
            TokenValue = _token.GenerateToken(currentUser)
        };

        return response;
    }
}


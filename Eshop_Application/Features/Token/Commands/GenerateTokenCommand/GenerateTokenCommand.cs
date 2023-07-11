using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.TokenEntities;
using Eshop_Domain.Entities.UserEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.TokenExeption.Commands.GenerateTokenCommand;

public sealed record GenerateTokenCommand(string token):IRequest<TokenResponse>;

public sealed class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, TokenResponse>
{
    private readonly ITokenRepository _token;
    private readonly IUserRepository _user;
    private readonly double sixMonthsExpirationTime = 6 * 43830; // 1 month in minutes => 43830 minutes 

    public GenerateTokenCommandHandler(
        ITokenRepository token,
        IUserRepository user)
    {
        _token = token;
        _user = user;
    }
    public async Task<TokenResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal tokenClaims = _token.ValidateAndReturnTokenClaims(request.token);

        string? userName = tokenClaims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        if (userName is null) throw new TokenException("Invalid Token");

        User? currentUser = await _user.GetUserByUsername(userName);

        if (currentUser is null) throw new NotFoundException($"The user <{userName}> was not found");

        TokenResponse response =  new TokenResponse()
        {
            TokenValue = _token.GenerateToken(currentUser,sixMonthsExpirationTime)
        };

        return response;
    }
}

using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.User.Commands.GetUserByUsername
{
    public record GetUserByUsernameCommand(string username):IRequest<UserDto?>;

    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameCommand, UserDto?>
    {
        private readonly IUserRepository _user;

        public GetUserByUsernameHandler(IUserRepository user)
        {
            _user = user;
        }
        public async Task<UserDto?> Handle(GetUserByUsernameCommand request, CancellationToken cancellationToken)
        {
            return await _user.GetUserByUsername(request.username);
        }
    }

}

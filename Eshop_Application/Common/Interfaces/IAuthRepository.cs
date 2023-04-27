using Eshop_Application.Features.User.Commands.LoginUserCommand;
using Eshop_Application.Features.User.Commands.RegisterUserCommand;
using Eshop_Domain.Entities.TokenEntities;
using Eshop_Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IAuthRepository 
    {
        Task CreateUserAsync(CreateUserCommand create);

        Task<TokenResponse> LoginUserAsync(LoginUserCommand loginUser);
    }
}

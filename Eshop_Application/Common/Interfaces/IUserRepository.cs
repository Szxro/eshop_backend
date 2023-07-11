using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IUserRepository 
    {
        Task<User?> GetUserByUsername(string username);

        void CreateUser(User newUser,string userHash,CancellationToken token);

        string? GetUserHash(User user);

        byte[]? GetUserSalt(User user);

        void ChangeUserStateToModified(User currentUser); 
    }
}

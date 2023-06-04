using Eshop_Domain.Dtos;
using Eshop_Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserInfoByEmail(string Email);

        Task<UserDto?> GetUserByUsername(string username);
    }
}

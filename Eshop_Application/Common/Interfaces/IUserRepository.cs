﻿using Eshop_Domain.DTOS;
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
        Task<User?> GetUserByUsername(string username);

        Task CreateUser(User newUser,string userHash,CancellationToken token);

        string? GetUserHash(User user);

        byte[]? GetUserSalt(User user);
    }
}

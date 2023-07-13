using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Exceptions;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop_Domain.DTOS;

namespace Eshop_Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void CreateUser(User newUser,string userHash, CancellationToken token)
        {
            _context.User.Add(newUser);

            _context.User.Entry(newUser).Property<string>("UserHash").CurrentValue = userHash;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            //Must be Track to get the shadow prop
            User? user = await _context.User
                                        .Include(x => x.UserUserRoles)
                                            .ThenInclude(x => x.UserRoles) // Including the UserRoles (else is going to be null)
                                        .Include(x => x.UserSalts)
                                        .AsSplitQuery() // Split Query is used when you do some inner joins (improve perfomance)
                                        .FirstOrDefaultAsync(x => x.UserName == username);

            return user; 
        }

        public string? GetUserHash(User user)
        {
            return _context.Entry(user).Property<string>("UserHash").CurrentValue;
        }

        public byte[]? GetUserSalt(User user)
        {
            return user.UserSalts.Select(x => Convert.FromHexString(x.SaltValue)).FirstOrDefault();
        }
    }
}

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

namespace Eshop_Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserInfoByEmail(string email)
        {
            //Must be Track to get the shadow prop
            User? user = await _context.User
                                      .Include(x => x.UserRoles)
                                      .Include(x => x.UserSalts)
                                      .AsSplitQuery() // Split Query is used when you do some inner joins (improve perfomance)
                                      .FirstOrDefaultAsync(x => x.Email == email);

            return user ?? throw new NotFoundException("The user was not found");
            // Using the nullable operator ?? 
        }
    }
}

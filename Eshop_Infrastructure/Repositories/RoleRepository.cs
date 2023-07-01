using Eshop_Domain.Entities.UserEntities;
using Eshop_Application.Common.Interfaces;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop_Infrastructure.Common;
using Eshop_Application.Common.Exceptions;

namespace Eshop_Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<UserRoles?> GetRoleByName(string roleName, CancellationToken token)
        {
            UserRoles? role = await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == roleName,token);

            return role;
        }
    }
}

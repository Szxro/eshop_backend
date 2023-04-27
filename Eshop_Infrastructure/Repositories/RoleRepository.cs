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
    public class RoleRepository : GenericRepository<UserRoles>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<UserRoles?> GetRoleByName(string name)
        {
            UserRoles? role = await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);

            return role ?? throw new NotFoundException("The role was not found");
        }
    }
}

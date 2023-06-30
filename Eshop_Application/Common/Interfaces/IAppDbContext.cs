using Eshop_Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> User {get;}

        DbSet<RefreshTokenUser> RefreshTokenUser { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

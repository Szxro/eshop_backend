using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Common;
using Eshop_Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Persistence.Interceptor
{
    public class AppDbInterceptor : IAppDbInterceptor
    {
        private readonly IDateTime _time;

        public AppDbInterceptor(IDateTime time)
        {
            _time = time;
        }
        public void UpdateAuditableEntities(DbContext? context)
        {
            if (context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            //Just the entities that changes state(Added or Modified) is going to be here
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = _time.Now();
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifyAt = _time.Now();
                }
            }
        }
    }
}

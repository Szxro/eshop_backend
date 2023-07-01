using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDateRepository _time;

        public UnitOfWork(
            AppDbContext context,
            IDateRepository time)
        {
            _context = context;
            _time = time;
        }
        public void UpdateAuditableEntities()
        {
            var entities = _context.ChangeTracker.Entries<AuditableEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
             //Just the entities that changes state(Added or Modified) is going to be here

            foreach (var entry in entities)
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

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           UpdateAuditableEntities();
           return _context.SaveChangesAsync(cancellationToken);
        }

    }
}

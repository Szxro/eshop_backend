using Eshop_Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Persistence.Configuration
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            List<UserRoles> newRoles = new()
            {
                new UserRoles(){Id = 1,Name = "Customer", NormalizedName = "Customer",CreatedAt = DateTime.Now,ModifyAt = DateTime.Now},
                new UserRoles(){Id = 2,Name = "Administrator", NormalizedName = "Administrator",CreatedAt = DateTime.Now,ModifyAt = DateTime.Now}
            };

            builder.HasData(newRoles);
        }
    }
}

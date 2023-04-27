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
    public class RefreshTokenUserConfiguration : IEntityTypeConfiguration<RefreshTokenUser>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenUser> builder)
        {
            builder.HasKey(x => new {x.UserId,x.UserRefreshTokenId });
        }
    }
}

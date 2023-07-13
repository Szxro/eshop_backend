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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Shadow Prop
            builder.Property<string>("UserHash");

            // Making some constrainsts
            builder.HasIndex(x => x.UserName).IsUnique(); // CONSTRAINST constraint_name UNIQUE(property)
            builder.HasIndex(x => x.Email).IsUnique(); // the props have to be unique in the db 
        }
    }
}

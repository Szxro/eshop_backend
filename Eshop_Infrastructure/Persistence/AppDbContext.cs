using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Common;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Eshop_Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Loading the Configuration of the entities
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //Applying the conventions
            configurationBuilder.Properties<string>().HaveMaxLength(256);
        }

        public DbSet<User> User => Set<User>();

        public DbSet<UserSalt> UserSalt => Set<UserSalt>();

        public DbSet<UserRoles> UserRoles => Set<UserRoles>();

        public DbSet<UserUserRoles> UserUserRoles => Set<UserUserRoles>();

        public DbSet<UserShippingInfo> UserShippingInfo => Set<UserShippingInfo>();

        public DbSet<Product> Product => Set<Product>();

        public DbSet<ProductCategory> ProductCategory => Set<ProductCategory>();

        public DbSet<ProductFile> ProductFile => Set<ProductFile>();

        public DbSet<UserCart> UserCart => Set<UserCart>();

    }
}
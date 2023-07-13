using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            //Creating a scope instance
            using var scope = app.Services.CreateScope();

            //Creating a service instance
            var initializer = scope.ServiceProvider.GetRequiredService<IAppDbContextInitializer>();

            await initializer.InitializeAsync();

            await initializer.SeedAsync();
        }
    }

    public class AppDbContextInitializer : IAppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppDbContextInitializer> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AppDbContextInitializer(
            AppDbContext context,
            ILogger<AppDbContextInitializer> logger,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task InitializeAsync()
        {
            try
            {
                //Making the migration if it find a new migration
                await _context.Database.MigrateAsync();
            }catch(Exception ex)
            {
                _logger.LogError("An error ocurred while initializing the database <{message}>",ex.Message);

                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            } catch (Exception ex)
            {
                _logger.LogError("An error occured while trying to seed the database <{message}>",ex.Message);
                throw;
            }
        }


        private async Task TrySeedAsync() 
        {
            //Seed if its necessary
            if (!_context.UserRoles.Any())
            {
                List<UserRoles> defaultRoles = new List<UserRoles>()
                {
                     new UserRoles(){Name = "Customer", NormalizedName = "Customer",},
                     new UserRoles(){Name = "Administrator", NormalizedName = "Administrator"}
                     //dont need to implement the id like hasData configuration (it will throw an identity column exception)
                };

                _context.UserRoles.AddRange(defaultRoles);

                await _unitOfWork.SaveChangesAsync();  
            }
        }
    }
}

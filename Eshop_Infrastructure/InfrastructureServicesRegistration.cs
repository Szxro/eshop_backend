using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Settings;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Eshop_Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {

            //DbContext Options
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("default")));  

            //Dependency Injection
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();   
            services.AddTransient<IDateRepository, DateRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IValidatorInput, ValidatorRepository>();
            services.AddTransient<IProductFileRepository, ProductFileRepository>();
            services.AddTransient<IRefreshTokenUserRepository, RefreshTokenUserRepository>();
            services.AddTransient<IPasswordRepository, PasswordRepository>();

            //Adding the Options
            services.Configure<JwtSettings>(options => config.GetSection("JWTConfig").Bind(options));


            return services;
        }

        public static IServiceCollection AddInfrastructureAuthentication(this IServiceCollection services,IConfiguration config)
        {
            //Adding the Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWTConfig:ValidIssuer"],
                    ValidAudience = config["JWTConfig:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWTConfig:SecretKey"]!))
                };
            });

            return services;
        }
    }
}

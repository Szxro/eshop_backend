using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Mapping;
using Eshop_Application.Features.User.Commands.LoginUserCommand;
using Eshop_Application.Features.User.Commands.RegisterUserCommand;
using Eshop_Domain.Entities.TokenEntities;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IRoleRepository _role;
        private readonly IPasswordUtilities _password;
        private readonly IUserRepository _user;
        private readonly ITokenRepository _token;
        private readonly AppDbContext _context;

        public AuthRepository(
            IRoleRepository role,
            IPasswordUtilities password,
            IUserRepository user,
            ITokenRepository token,
            AppDbContext context) 
        {
            _role = role;
            _password = password;
            _user = user;
            _token = token;
            _context = context;
        }

        public async Task CreateUserAsync(CreateUserCommand createUser)
        {
            //Checking if the password (need to be equal)
            _password.verifyPasswordsEquality(createUser.Password, createUser.ConfirmPassword);

            //Getting the userRole
            UserRoles? customerRole = await _role.GetRoleByName("Customer");

            //Generating the user hash and salt
            string userHash = _password.GenerateUserHashAndSalt(createUser.Password,out byte[] userSalt);

            // TODO: Use AutoMapper
            User newUser = createUser.ToUser(userSalt,customerRole!.Id);

            newUser.UserRoles.Select(x => _context.Entry(x).State = EntityState.Unchanged).ToList();

            _context.User.Add(newUser);

            _context.User.Entry(newUser).Property<string>("UserHash").CurrentValue = userHash;

            await _context.SaveChangesAsync();
        }

        public async Task<TokenResponse> LoginUserAsync(LoginUserCommand loginUser)
        {
            User? userFound = await _user.GetUserInfoByEmail(loginUser.Email);

            //Getting the hash
            string UserHash = _context.Entry(userFound!).Property<string>("UserHash").CurrentValue; 

            //?? null

            //Getting the Salt
            byte[]? userSalt = userFound!.UserSalts.Select(x => Convert.FromHexString(x.SaltValue)).FirstOrDefault();

            //?? null 

            //Checking the password
            bool IsCorrectPassword = _password.VerifyPasswordHash(loginUser.Password,UserHash,userSalt!);

            if (!IsCorrectPassword)
            {
                throw new ArgumentException("The Password is incorrect");
            }

            return new TokenResponse()
            {
                TokenValue = _token.GenerateToken(userFound),
                RefreshTokenValue = _token.GenerateRefreshToken()
            };
        }
    }
}

using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class RefreshTokenUserRepository : IRefreshTokenUserRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenUserRepository(AppDbContext context)
        {
            _context = context;
        }
        public string GenerateRefreshToken(int length = 20)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            // this return an array of char and later its transform into an array
            return new string(Enumerable.Repeat(chars, length).Select(value => value[random.Next(value.Length)]).ToArray());
        }

        public void SaveUserRefreshToken(User user, string refreshToken)
        {
            //Changing the state
            _context.Entry(user).State = EntityState.Unchanged;

            RefreshTokenUser tokenUser = new()
            {
                User = user,
                UserRefreshToken = new UserRefreshToken()
                {
                    IsActive = false,
                    RefreshTokenValue = refreshToken
                }
            };

            _context.RefreshTokenUser.Add(tokenUser);
        }
    }
}

using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Settings;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Eshop_Domain.Entities.TokenEntities;

namespace Eshop_Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JwtSettings _jwtSettings;
        private readonly AppDbContext _context;

        public TokenRepository(IOptions<JwtSettings> options,AppDbContext context)
        {
            _jwtSettings = options.Value;
            _context = context;
        }

        public string GenerateToken(User user)
        {
            //Initialiazing the Handler
            JwtSecurityTokenHandler handler = new();

            //Encoding the Key
            byte[] key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            //Making the Token Descriptor
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _jwtSettings.ValidIssuer,
                Audience = _jwtSettings.ValidAudience,
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiresIn)),
                SigningCredentials = new SigningCredentials(key: new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha512)
            };

             //Creating the token
             SecurityToken token = handler.CreateToken(tokenDescriptor);

             //Returning the token in string
             return handler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            List<Claim> userRoles = user.UserUserRoles
                                                .Select(x => x.UserRoles)
                                                .Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();

            if (userRoles is null)
            {
                throw new NotFoundException("The roles of the user was not found");
            }

            ClaimsIdentity userClaims = new ClaimsIdentity(new[]
            {
                new Claim("uid",$"{user.Id}"),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) // Unique Guid by Users
            }
            .Union(userRoles)); // Union return an IEnumerable

            return userClaims;
        }
    }
}

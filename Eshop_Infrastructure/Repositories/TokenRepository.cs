using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Settings;
using Eshop_Domain.Entities.UserEntities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JwtSettings _jwtSettings;
        public TokenRepository(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        public string GenerateRefreshToken(int length = 20)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars,length).Select(value => value[random.Next(value.Length)]).ToArray());
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            List<Claim> userRoles = user.UserRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();

            if (userRoles is null)
            {
                throw new NotFoundException("The user roles was not found");
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
    }
}

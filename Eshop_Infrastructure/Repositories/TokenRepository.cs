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
        private readonly TokenValidationParameters _parameters;
        private readonly IDateRepository _date;

        public TokenRepository(IOptions<JwtSettings> options,IDateRepository date)
        {
            _jwtSettings = options.Value;
            _parameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = false, // to not have a time exception 
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.ValidIssuer,
                ValidAudience = _jwtSettings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey)),
            };
            _date = date;
        }

        public string GenerateToken(User user, double tokenLifeTime = 10.00)// default time for the token to expire 10 minutes
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
                Expires = DateTime.UtcNow.AddMinutes(tokenLifeTime),
                SigningCredentials = new SigningCredentials(key: new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha512)
                // NotBefore => future
            };

             //Creating the token
             SecurityToken token = handler.CreateToken(tokenDescriptor);

             //Returning the token in string
             return handler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateAndReturnTokenClaims(string token)
        {
            JwtSecurityTokenHandler handler = new();

            ClaimsPrincipal tokenClaims = handler.ValidateToken(token, _parameters, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                bool isTokenAlgorithmValid = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);

                if (!isTokenAlgorithmValid) throw new TokenException("Invalid Token Algorithm");
            }

            string? tokenExpiryStamp = tokenClaims.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;

            if (tokenExpiryStamp is null) throw new TokenException("Invalid Token Stamp");

            DateTime tokenExpiryDate = _date.TimeStampToUTCDate(long.Parse(tokenExpiryStamp));

            if (tokenExpiryDate > DateTime.UtcNow) throw new TokenException("The token is still valid");

            return tokenClaims;
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

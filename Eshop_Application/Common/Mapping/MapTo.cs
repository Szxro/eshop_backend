using Eshop_Application.Features.User.Commands.RegisterUserCommand;
using Eshop_Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Mapping
{
    public static class MapTo
    {
        public static UserShippingInfo ToUserShippingInfo(this CreateUserShippingInfo request)
        {
            return new UserShippingInfo()
            {
                Address = request.Address,
                Country = request.Country,
                Phone = request.Phone,
                State = request.State,
                City = request.City,
                PostalCode = request.PostalCode,
                CountryCode = request.CountryCode
            };
        }

        public static User ToUser(this CreateUserCommand request, byte[] salt,int roleId)
        {
            return new User()
            {
                UserName = request.UserName,
                NormalizedUsername = request.UserName, // TODO: Use the username and email to normalized it 
                NormalizedEmail = request.Email,
                Email = request.Email,
                LockOutEnd = new DateTime(1999,01,01,12,00,00), //Default data
                AccessFailedCount = 0,
                LockOutEnable = true,
                UserRoles = new List<UserRoles>()
                {
                    new UserRoles() { Id = roleId }
                },
                UserSalts = new List<UserSalt>()
                {
                    new UserSalt() { SaltValue = Convert.ToHexString(salt) }
                },
                UserShippingInfos = request.UserShippingInfo.Select(x => x.ToUserShippingInfo()).ToList() 
            };
        }
    }
}

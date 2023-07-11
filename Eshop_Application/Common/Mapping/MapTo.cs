using Eshop_Application.Features.Products.Commands.CreateProductCommand;
using Eshop_Application.Features.Users.Commands.CreateUserCommand;
using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.ProductEntities;
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

        public static Product ToProduct(this CreateProductCommand request)
        {
            return new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                ProductPrice = request.ProductPrice,
                ProductCategory = request.ProductCategory
                                         .Select(x => new ProductCategory() { CategoryName = x.CategoryName }).ToList(),
                ProductQuantity = request.ProductQuantity,
            };
        }

        public static ProductDTO ToProductDTO(this Product request)
        {
            return new ProductDTO()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                ProductPrice = request.ProductPrice,
                ProductCategories = request.ProductCategory
                                         .Select(x => new ProductCategoryDTO() { CategoryName = x.CategoryName }).ToList(),
                ProductQuantity = request.ProductQuantity,
                ProductFiles = request.ProductFile
                                          .Select(x => new ProductFileDTO
                                          {
                                              FileTempName = x.FileTempName,
                                              FileRealName = x.FileRealName,
                                              FileContentType = x.FileContentType,
                                              FileLength = x.FileLength,
                                              FilePath = x.FilePath,
                                          }).ToList()
            };
        }


        public static User ToUser(this CreateUserCommand request, byte[] salt,UserRoles roles)
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
                UserUserRoles = new List<UserUserRoles>(){ new UserUserRoles() { UserRoles = roles} },
                UserSalts = new List<UserSalt>()
                {
                    new UserSalt() { SaltValue = Convert.ToHexString(salt) }
                },
                UserShippingInfos = request.UserShippingInfo.Select(x => x.ToUserShippingInfo()).ToList() 
            };
        }
    }
}

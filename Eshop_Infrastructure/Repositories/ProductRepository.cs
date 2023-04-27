using Eshop_Application.Common.Interfaces;
using Eshop_Application.Features.Product.Commands.CreateProductCommand;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductIRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateProduct(CreateProductCommandDTO request)
        {
            // TODO: Use Mapper for this

            Product product = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                ProductImageUrl = request.ProductImageUrl,
                ProductPrice = request.ProductPrice,
                ProductQuantity = request.ProductQuantity
            };

            _context.Product.Add(product);

            await _context.SaveChangesAsync();
        }
    }
}

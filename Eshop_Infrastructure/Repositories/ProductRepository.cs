using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Mapping;
using Eshop_Application.Features.Products.Commands.CreateProductCommand;
using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateProduct(CreateProductCommand request)
        {
            Product newProduct =  request.ToProduct();

            _context.Add(newProduct);
        }

        public async Task<Product?> GetProductByProductName(string productName)
        {
            Product? product = await _context.Product.FirstOrDefaultAsync(x => x.ProductName == productName);

            return product ?? throw new ProductException("The Product was not found");
        }

        public async Task<bool> CheckProductNameExists(string productName)
        {
            return await _context.Product.AnyAsync(p => p.ProductName == productName);
        }

        public void ChangeProductStateToUnchanged(Product product)
        {
            _context.Entry(product).State = EntityState.Unchanged;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.Include(x => x.ProductCategory).AsNoTracking().ToListAsync();
        }
    }
}

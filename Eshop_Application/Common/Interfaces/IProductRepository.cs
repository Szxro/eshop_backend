using Eshop_Application.Features.Products.Commands.CreateProductCommand;
using Eshop_Domain.Entities.ProductEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IProductRepository 
    {
        void CreateProduct(CreateProductCommand request);

        Task<Product?> GetProductByProductName(string productName);

        Task<List<Product>> GetAllProducts();

        Task<bool> CheckProductNameExists(string productName);

        void ChangeProductStateToUnchanged(Product product);
    }
}

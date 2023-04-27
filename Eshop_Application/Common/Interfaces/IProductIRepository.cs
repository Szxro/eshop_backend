using Eshop_Application.Features.Product.Commands.CreateProductCommand;
using Eshop_Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IProductIRepository : IGenericRepository<Product>
    {
        Task CreateProduct(CreateProductCommandDTO request);
    }
}

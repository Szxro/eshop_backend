using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Mapping;
using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.ProductEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Queries.GetAllProductsQuery
{
    public record GetAllProductsQuery():IRequest<List<ProductDTO>> { }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _product;

        public GetAllProductQueryHandler(IProductRepository product)
        {
            _product = product;
        }
        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> allProducts = await _product.GetAllProducts();

            return allProducts.Select(x => x.ToProductDTO()).ToList();
        }
    }
}

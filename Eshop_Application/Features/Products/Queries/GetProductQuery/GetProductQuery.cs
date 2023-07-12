using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Mapping;
using Eshop_Application.Pagination;
using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.ProductEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Queries.GetProductQuery
{
    public record GetProductQuery(
        string? searhTerm,
        string? sortColumn,
        string? sortOrder,
        int offset, 
        int limit
        ):IRequest<PaginatedList<ProductDTO>> { }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, PaginatedList<ProductDTO>>
    {
        private readonly IAppDbContext _context;

        public GetProductQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> productQuery = _context.Product.AsQueryable(); // Building a query

            if (!string.IsNullOrWhiteSpace(request.searhTerm))
            {
                productQuery = productQuery.Where(x => x.ProductName.Contains(request.searhTerm)); // Filtering 
            }

            productQuery = request.sortOrder?.ToLower() == "desc" ? // Sorting And Ordering
                           productQuery.OrderByDescending(GetSortColumn(request.sortColumn)) : productQuery.OrderBy(GetSortColumn(request.sortColumn));


            IQueryable<ProductDTO> productDTOQuery = productQuery
                                                    .Include(x => x.ProductCategory)
                                                    .Include(x => x.ProductFile)
                                                    .AsNoTracking().Select(x => x.ToProductDTO());


            PaginatedList<ProductDTO> products = await PaginatedList<ProductDTO>.Create(productDTOQuery,request.limit,request.offset); // Pagination

            return products;
        }

        private Expression<Func<Product, object>> GetSortColumn(string? sortColumn)
        {
            // Func => return something 
            Expression<Func<Product, object>> keySelector = sortColumn?.ToLower() switch 
            {
                "name" => product => product.ProductName, // "posible_value => parameter_name => what_is_going_to_return"
                "quantity" => product => product.ProductQuantity,
                "price" => product => product.ProductPrice,
                "description" => product => product.Description, 
                _ => product => product.ProductName
            }; // sortColumn is null , default value is ProductName

            return keySelector; 
        }
    }
}

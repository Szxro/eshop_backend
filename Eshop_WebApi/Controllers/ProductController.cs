using Eshop_Application.Common.Interfaces;
using Eshop_Application.Features.Products.Commands.CreateProductCommand;
using Eshop_Application.Features.Products.Commands.CreateProductFileCommand;
using Eshop_Application.Features.Products.Queries.GetProductQuery;
using Eshop_Application.Pagination;
using Eshop_Domain.DTOS;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Domain.Entities.UserEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eshop_WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("get/products")]

        public async Task<ActionResult<PaginatedList<ProductDTO>>> GetAllProducts(string? searhTerm,string? sortColumn,string? sortOrder,int limit = 1,int offset = 1)
        {
            return Ok(await _mediator.Send(new GetProductQuery(searhTerm,sortColumn,sortOrder,offset,limit)));
        }

        [HttpPost("post/newproduct")]

        public async Task<ActionResult<Unit>> PostNewProduct([FromBody] CreateProductCommand request)
        {

           return Ok(await _mediator.Send(request));
        }

        [HttpPost("post/uploadproductimage")]

        public async Task<ActionResult<Unit>> PostProductImage([FromQuery] string productName, List<IFormFile>? files)
        {
            return Ok(await _mediator.Send(new CreateProductFileCommand(productName, files))); 
        }
    }
}

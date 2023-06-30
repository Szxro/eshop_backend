using Eshop_Application.Common.Interfaces;
using Eshop_Application.Features.Products.Commands.CreateProductCommand;
using Eshop_Domain.DTOS;
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
        private readonly IProductRepository _product;

        public ProductController(IMediator mediator, IProductRepository product)
        {
            _mediator = mediator;
            _product = product;
        }

        [HttpPost("post/newproduct")]

        public async Task<ActionResult> PostNewProduct([FromBody] CreateProductCommand request)
        {
           var result = await _mediator.Send(request);

           return Ok(result);
        }

        [HttpPost("post/uploadproductimage")]

        public async Task<ActionResult> PostProductImage([FromQuery] string productName, List<IFormFile>? files)
        {
            await _product.UploadProductImageByProductNameAsync(productName, files);

            return NoContent(); 
        }
    }
}

using Eshop_Application.Features.TokenExeption.Commands.GenerateTokenCommand;
using Eshop_Domain.Entities.TokenEntities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("post/regeneratetoken")]

        public async Task<ActionResult<TokenResponse>> ReGenerateTokenAndRefreshToken(TokenResponse request)
        {
            return Ok(await _mediator.Send(new GenerateTokenCommand(request.TokenValue)));
        }
    }
}

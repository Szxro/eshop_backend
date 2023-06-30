using Eshop_Application.Features.Users.Commands.LoginUserCommand;
using Eshop_Application.Features.Users.Commands.CreateUserCommand;
using Eshop_Domain.Entities.TokenEntities;
using Eshop_WebApi.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eshop_WebApi.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("post/register")]
        [ProducesDefaultResponseType]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Unit>> CreateUserAsync(CreateUserCommand createUser,CancellationToken token)
        {
            var result = await _mediator.Send(createUser,token);

            return Ok(result);
        }

        [HttpPost("post/login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenResponse>> LoginUserAsync(LoginUserCommand loginUser)
        {
            return Ok(await _mediator.Send(loginUser));
        }
    }
}

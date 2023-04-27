using Eshop_Application.Features.User.Commands.LoginUserCommand;
using Eshop_Application.Features.User.Commands.RegisterUserCommand;
using Eshop_Domain.Entities.TokenEntities;
using Eshop_WebApi.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eshop_WebApi.Controllers
{
    [Route("api/v1/[controller]")]
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
        public async Task<ActionResult<Unit>> CreateUserAsync(CreateUserCommand createUser)
        {
            await _mediator.Send(createUser);
            return NoContent();
        }

        [HttpPost("post/login")]
        [TokenAttribute]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenResponse>> LoginUserAsync(LoginUserCommand loginUser)
        {
            return Ok(await _mediator.Send(loginUser));
        }

        [HttpGet("get/logout")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesDefaultResponseType]
        [LogoutAttribute]
        public ActionResult LogOut()
        {
            return NoContent();
        }
    }
}

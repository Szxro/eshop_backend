using Eshop_Application.Features.Users.Commands.GetCurrentUserByUsernameCommand;
using Eshop_Domain.DTOS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Eshop_WebApi.Controllers
{
    [Route("api/v1/user")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/currentuser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CurrentUserDTO?>> GetUserByUsername()
        {
            return await _mediator.Send(new GetCurrentUserByUsername(User.FindFirstValue(ClaimTypes.Name)!));
        }
    }
}

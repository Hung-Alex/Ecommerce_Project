using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/authencations")]
    public class AuthencationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthencationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            SetCookies(result.AccessToken, result.RefreshToken);
            return Ok(result);
        }
        private void SetCookies(string accessToken, string refreshToken)
        {

            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        }
    }
}

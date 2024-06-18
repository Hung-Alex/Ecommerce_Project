using Application.DTOs.Internal.Authen;
using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.Refresh;
using Application.Features.Authen.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using System.Runtime.InteropServices;

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
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            SetCookies(result.AccessToken, result.RefreshToken);
            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken()
        {
            if (!(Request.Cookies.ContainsKey("X-Access-Token")
                && Request.Cookies.ContainsKey("X-Refresh-Token"))) return BadRequest("Not Found Token in Cookies");
            var refreshToken = Request.Cookies.FirstOrDefault(x => x.Key == "X-Refresh-Token");
            var accessToken = Request.Cookies.FirstOrDefault(x => x.Key == "X-Access-Token");
            var result = await _mediator.Send(new RefreshTokenCommand(accessToken.Value, refreshToken.Value));
            SetCookies(result.AccessToken, result.RefreshToken);
            return Ok(result);

        }
        private void SetCookies(string accessToken, string refreshToken)
        {

            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true });
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        }
    }
}

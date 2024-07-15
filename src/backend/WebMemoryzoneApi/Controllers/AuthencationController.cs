using Application.Common.Interface;
using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.Refresh;
using Application.Features.Authen.Commands.Register;
using Application.Features.Authen.Queries.GetGoogleLoginUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/authentications")]
    public class AuthencationController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        private readonly IMediator _mediator;
        public AuthencationController(IMediator mediator, ISectionService sectionService)
        {
            _sectionService = sectionService;
            _mediator = mediator;
        }
        [HttpGet("sign-in-google")]
        public async Task<IActionResult> SignInWithGoole(string code,string state)
        {
            return Ok(code);
        }
        [HttpGet("get-login-google-url")]
        public async Task<IActionResult> GetLoginGoogleUrl()
        {
            var result = await _mediator.Send(new GetGoogleLoginUrlQuery());
            if (result.IsSuccess is false)
            {
                return NotFound(result);
            }
            return Ok(result);

        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken, result.Data.RefreshToken);
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
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken, result.Data.RefreshToken);
            return Ok(result);

        }      
        private void SetCookies(string accessToken, string refreshToken)
        {

            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true });
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        }
    }
}

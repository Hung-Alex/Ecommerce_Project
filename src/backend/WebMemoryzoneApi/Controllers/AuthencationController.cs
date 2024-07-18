using Application.Common.Interface.IdentityService;
using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.LoginWithGoogle;
using Application.Features.Authen.Commands.Refresh;
using Application.Features.Authen.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/authentications")]
    public class AuthencationController : ControllerBase
    {
        private readonly IGoogleAuthenService _googleAuthenService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private static readonly HttpClient httpClient = new HttpClient();
        public AuthencationController(IMediator mediator, IGoogleAuthenService googleAuthenService, IConfiguration configuration)
        {
            _googleAuthenService = googleAuthenService;
            _mediator = mediator;
            _configuration = configuration;

        }
        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInWithGoole([FromBody]LoginGoogleCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken, result.Data.RefreshToken, result.Data.User.Name);
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
            SetCookies(result.Data.AccessToken, result.Data.RefreshToken,result.Data.User.Name);
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
            SetCookies(result.Data.AccessToken, result.Data.RefreshToken,result.Data.User.Name);
            return Ok(result);

        }
        private void SetCookies(string accessToken, string refreshToken,string userName)
        {

            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, IsEssential = true });
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, IsEssential = true });
            Response.Cookies.Append("User-Name-App", userName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, IsEssential = true });

        }
    }
}
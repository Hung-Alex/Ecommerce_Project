using Application.DTOs.Internal;
using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.LoginWithGoogle;
using Application.Features.Authen.Commands.Logout;
using Application.Features.Authen.Commands.Refresh;
using Application.Features.Authen.Commands.Register;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/authentications")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;
        public AuthenticationController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
            _jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new ArgumentNullException("not config jwt ");

        }
        [HttpPost("sign-in-google")]
        public async Task<IActionResult> SignInWithGoole([FromBody] LoginGoogleCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken
                  , result.Data.RefreshToken
                  , result.Data.User.Name
                  , result.Data.User.ImageUrl
                  , DateTime.Now.AddMinutes(_jwtSetting.ExpiredToken)
                  , DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken));
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
            SetCookies(result.Data.AccessToken
                 , result.Data.RefreshToken
                 , result.Data.User.Name
                 , result.Data.User.ImageUrl
                 , DateTime.Now.AddMinutes(_jwtSetting.ExpiredToken)
                 , DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken));
            return Ok(result);
        }
        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            if (!(Request.Cookies.ContainsKey(UserToken.AccessTokenCookies)
                && Request.Cookies.ContainsKey(UserToken.RefreshTokenCookies))) return BadRequest("Not Found Token in Cookies");
            var refreshToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.RefreshTokenCookies);
            var accessToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.AccessTokenCookies);
            var result = await _mediator.Send(new LogoutCommand(accessToken.Value));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(""
                , ""
                , ""
                , ""
                , DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-100));
            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken()
        {
            if (!(Request.Cookies.ContainsKey(UserToken.AccessTokenCookies)
                && Request.Cookies.ContainsKey(UserToken.RefreshTokenCookies))) return BadRequest("Not Found Token in Cookies");
            var refreshToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.RefreshTokenCookies);
            var accessToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.AccessTokenCookies);
            var result = await _mediator.Send(new RefreshTokenCommand(accessToken.Value, refreshToken.Value));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken
                , result.Data.RefreshToken
                , result.Data.User.Name
                , result.Data.User.ImageUrl
                , DateTime.Now.AddMinutes(_jwtSetting.ExpiredToken)
                , DateTime.Now.AddDays(_jwtSetting.ExpiredRefreshToken)
                );
            return Ok(result);
        }
        private void SetCookies(string accessToken, string refreshToken, string userName, string image, DateTime expiredTimeAccestoken, DateTime expiredTimeRefreshToken)
        {
            Response.Cookies.Append(UserToken.UserInfoNoneBlock, userName, new CookieOptions() { SameSite = SameSiteMode.None, Secure = true, IsEssential = true, Expires = expiredTimeAccestoken });
            Response.Cookies.Append(UserToken.ImageDefault, image ?? UserToken.ImageDefaultValue, new CookieOptions() { SameSite = SameSiteMode.None, IsEssential = true, Secure = true, Expires = expiredTimeAccestoken });
            Response.Cookies.Append(UserToken.AccessTokenCookies, accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, IsEssential = true, Expires = expiredTimeAccestoken });
            Response.Cookies.Append(UserToken.RefreshTokenCookies, refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, IsEssential = true, Expires = expiredTimeRefreshToken });
            Response.Cookies.Append(UserToken.UserInfoCookies, userName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, IsEssential = true, Expires = expiredTimeRefreshToken });
        }
    }
}
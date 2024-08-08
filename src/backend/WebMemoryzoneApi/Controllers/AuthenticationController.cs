using Application.DTOs.Internal;
using Application.DTOs.Internal.User;
using Application.DTOs.Responses.Auth;
using Application.DTOs.Responses.Banners;
using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.LoginWithGoogle;
using Application.Features.Authen.Commands.Logout;
using Application.Features.Authen.Commands.Refresh;
using Application.Features.Authen.Commands.Register;
using Application.Helper;
using Domain.Constants;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


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
            _jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>() ?? throw new InvalidOperationException("not config jwt ");

        }
        /// <summary>
        /// Signs in using Google account.
        /// </summary>
        /// <param name="command">The command containing Google sign-in information.</param>
        /// <returns>
        /// A <see cref="IActionResult"/> containing the JWT tokens and user information if successful, otherwise a 400 Bad Request response.
        /// </returns>
        [HttpPost("sign-in-google")]
        [ProducesResponseType(typeof(Result<AuthencationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<AuthencationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignInWithGoole([FromBody] LoginGoogleCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken
                  , result.Data.RefreshToken
                  , result.Data.User
                  , JWTHelper.GetExpiresAccessToken(_jwtSetting.ExpiredToken)
                  , JWTHelper.GetExpiresRefreshToken(_jwtSetting.ExpiredRefreshToken));
            return Ok(result);
        }
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="command">The command containing registration information.</param>
        /// <returns>
        /// A <see cref="ActionResult"/> containing the registration result if successful, otherwise a 400 Bad Request response.
        /// </returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="command">The command containing login information.</param>
        /// <returns>
        /// A <see cref="ActionResult"/> containing the JWT tokens and user information if successful, otherwise a 400 Bad Request response.
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(Result<AuthencationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<AuthencationResponse>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken
                 , result.Data.RefreshToken
                 , result.Data.User
                 , JWTHelper.GetExpiresAccessToken(_jwtSetting.ExpiredToken)
                 , JWTHelper.GetExpiresRefreshToken(_jwtSetting.ExpiredRefreshToken));
            return Ok(result);
        }
        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>
        /// A <see cref="ActionResult"/> indicating whether the logout was successful.
        /// </returns>
        [HttpGet("logout")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Logout()
        {
            if (!(Request.Cookies.ContainsKey(UserToken.AccessTokenCookies)
                && Request.Cookies.ContainsKey(UserToken.RefreshTokenCookies))) return BadRequest(Result<bool>.ResultFailures(ErrorConstants.AuthenticationError.AuthCookiesNotFound));
            var refreshToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.RefreshTokenCookies);
            var accessToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.AccessTokenCookies);
            var result = await _mediator.Send(new LogoutCommand(accessToken.Value));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(""
                , ""
                , null
                , DateTime.Now.AddYears(-100)
                , DateTime.Now.AddYears(-100));
            return Ok(result);
        }
        /// <summary>
        /// Refreshes the JWT tokens.
        /// </summary>
        /// <returns>
        /// A <see cref="ActionResult"/> containing the refreshed JWT tokens if successful, otherwise a 400 Bad Request response.
        /// </returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(Result<AuthencationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<AuthencationResponse>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RefreshToken()
        {
            if (!(Request.Cookies.ContainsKey(UserToken.AccessTokenCookies)
                && Request.Cookies.ContainsKey(UserToken.RefreshTokenCookies))) return BadRequest(Result<bool>.ResultFailures(ErrorConstants.AuthenticationError.AuthCookiesNotFound));
            var refreshToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.RefreshTokenCookies);
            var accessToken = Request.Cookies.FirstOrDefault(x => x.Key == UserToken.AccessTokenCookies);
            var result = await _mediator.Send(new RefreshTokenCommand(accessToken.Value, refreshToken.Value));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            SetCookies(result.Data.AccessToken
                , result.Data.RefreshToken
                , result.Data.User
                , JWTHelper.GetExpiresAccessToken(_jwtSetting.ExpiredToken)
                , JWTHelper.GetExpiresRefreshToken(_jwtSetting.ExpiredRefreshToken)
                );
            return Ok(result);
        }
        private void SetCookies(string accessToken, string refreshToken, UserDTO user, DateTime expiredTimeAccestoken, DateTime expiredTimeRefreshToken)
        {
            var userInfo = JsonSerializer.Serialize(user);
            //can read
            Response.Cookies.Append(UserToken.AccessTokenCookiesCanRead, accessToken, new CookieOptions()
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = expiredTimeAccestoken,
            });
            Response.Cookies.Append(UserToken.RefreshTokenCookiesCanRead, refreshToken, new CookieOptions()
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = expiredTimeRefreshToken,
            });
            Response.Cookies.Append(UserToken.UserInfoNoneBlock, userInfo, new CookieOptions()
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = expiredTimeAccestoken,
            });
            //http only
            Response.Cookies.Append(UserToken.AccessTokenCookies, accessToken, new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = expiredTimeRefreshToken,
            });
            Response.Cookies.Append(UserToken.RefreshTokenCookies, refreshToken, new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = expiredTimeRefreshToken,
            });
        }
    }
}
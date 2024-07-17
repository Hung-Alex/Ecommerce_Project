using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Application.Features.Authen.Commands.Login;
using Application.Features.Authen.Commands.Refresh;
using Application.Features.Authen.Commands.Register;
using Application.Features.Authen.Queries.GetGoogleLoginUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;


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
        [HttpGet("sign-in-google")]
        public async Task<IActionResult> SignInWithGoole(string code, string state)
        {
            // var result = await _googleAuthenService.SignInByGoogleAsync(code);
            //return Ok(code);
            var _googleSettings = _configuration.GetSection("Google").Get<GoogleSettings>();


            var requestBody = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("client_id", _googleSettings.ClientId),
            new KeyValuePair<string, string>("client_secret", _googleSettings.ClientSecret),
            new KeyValuePair<string, string>("redirect_uri", _googleSettings.RedirectUri),
            new KeyValuePair<string, string>("grant_type", "authorization_code")
        });

            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error exchanging authorization code for tokens.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);

            var idToken = json.Value<string>("id_token");
            var accessToken = json.Value<string>("access_token");
            var result = await _googleAuthenService.SignInByGoogleAsync(idToken);
            // Xử lý ID token và access token theo nhu cầu của bạn
            // Ví dụ: Lưu trữ trong database, thiết lập cookie, v.v.

            return Ok(new { idToken, accessToken });
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
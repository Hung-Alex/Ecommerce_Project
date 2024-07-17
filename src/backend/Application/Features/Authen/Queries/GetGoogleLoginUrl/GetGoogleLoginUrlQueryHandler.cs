using Application.Common.Interface;
using Domain.Constants;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Authen.Queries.GetGoogleLoginUrl
{
    public sealed class GetGoogleLoginUrlQueryHandler : IRequestHandler<GetGoogleLoginUrlQuery, Result<string>>
    {
        private readonly GoogleSettings _googleSettings;
        private readonly IConfiguration _configuration;
        public GetGoogleLoginUrlQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _googleSettings = _configuration.GetSection("Google").Get<GoogleSettings>() ?? throw new ArgumentNullException();
        }

        public async Task<Result<string>> Handle(GetGoogleLoginUrlQuery request, CancellationToken cancellationToken)
        {
            var state = Guid.NewGuid().ToString();
            var scope = "openid profile email";
            string Url = LoginGoogleUrlConstants.GetUrl(_googleSettings.ClientId, scope, _googleSettings.RedirectUri, state);
            return Result<string>.ResultSuccess(Url);
        }
    }
}

using Domain.Shared;
using MediatR;

namespace Application.Features.Authen.Queries.GetGoogleLoginUrl
{
    public record GetGoogleLoginUrlQuery : IRequest<Result<string>>;
}

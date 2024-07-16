using Domain.Shared;

namespace Application.Common.Interface.IdentityService
{
    public interface IGoogleAuthenService
    {
        Task<Result<Guid>> SignInByGoogleAsync(string IdToken, CancellationToken cancellationToken = default);
    }
}


namespace Application.Common.Interface
{
    public interface IJwtProvider
    {
        Task<string> GenerateTokenAsync(Guid userId);
    }
}

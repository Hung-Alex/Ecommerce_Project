using Application.Common.Interface;
using Domain.Interface;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Repo
{
    internal static class DependencyInjectionReposioryExtension
    {
        internal static IServiceCollection AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}

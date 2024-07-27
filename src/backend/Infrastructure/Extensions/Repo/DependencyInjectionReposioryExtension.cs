using Application.Common.Interface;
using Application.Common.Interface.RepositoryExtension;
using Domain.Interface;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.Repository;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Repo
{
    internal static class DependencyInjectionReposioryExtension
    {
        internal static IServiceCollection AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICartRepositoryExtension, CartRepositoryExtension>();
            services.AddScoped<ICategoryRepositoryExtension, CategoryRepositoryExtension>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}

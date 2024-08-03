using Application.Common.Interface;
using Application.Common.Interface.RepositoryExtension;
using Domain.Interface;
using Infrastructure.Persistence.Repositories.GenericRepository;
using Infrastructure.Persistence.Repositories.Repository;
using Infrastructure.Persistence.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Extensions.Repo
{
    public static class DependencyInjectionReposioryExtension
    {
        public static IServiceCollection AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICartRepositoryExtension, CartRepositoryExtension>();
            services.AddScoped<ICategoryRepositoryExtension, CategoryRepositoryExtension>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}

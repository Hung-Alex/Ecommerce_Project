using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EcommerceDB")));
            // Register UnitOfWork
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            // Register Repository
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            return services;
        }
    }
}

using Infrastructure.Extensions.Services;
using Infrastructure.Persistence.Extensions.Repo;
using Infrastructure.Persistence.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EcommerceDB")));
            services.AddRepositoryExtension();
            services.AddServicesExtension();
            services.AddIdentityExtension();
            services.AddAuthencationExtension(configuration);
            services.AddCORSExtension();
            services.AddAuthorizationExtension();
            return services;
        }
    }
}

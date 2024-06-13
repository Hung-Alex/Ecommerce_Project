using Application.Common.Interface;
using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CloudinaryUpload;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            // Register Services
            services.AddScoped<IMedia, Media>();
            return services;
        }
    }
}

using Application.Common.Interface;
using Application.Common.Interface.IdentityService;
using Domain.Interface;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories.GenericRepository;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CloudinaryUpload;
using Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Identity;
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
            //register Identity service
            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            // Register Services
            services.AddScoped<IMedia, Media>();
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<StoreDbContext>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();
            services.AddScoped<IIdentityService, IdentityService>();


            return services;
        }
    }
}

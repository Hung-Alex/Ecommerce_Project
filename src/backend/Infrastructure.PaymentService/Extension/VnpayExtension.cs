using Application.Common.Interface.Payments;
using Infrastructure.PaymentService.VnPay.Handle;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.PaymentService.Extension
{
    public static class VnpayExtension
    {
        public static IServiceCollection AddVnpayExtension(this IServiceCollection services)
        {
            services.AddScoped<IVnPayService, VnPayService>();
            return services;
        }
    }
}


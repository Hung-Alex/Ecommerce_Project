using Application.Common.Interface.Payment;
using Infrastructure.PaymentService.VnPay.Handle;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.PaymentService.Extension
{
    public static class VnpayExtension
    {
        public static IServiceCollection AddVnpayExtension(this IServiceCollection services)
        {
            services.AddScoped<IPaymentStrategy, VnPayStrategy>();
            return services;
        }
    }
}


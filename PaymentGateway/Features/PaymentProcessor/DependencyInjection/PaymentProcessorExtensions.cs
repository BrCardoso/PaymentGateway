using Application.Features.PaymentProcessor.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Features.PaymentProcessor.DependencyInjection
{
    public static class PaymentProcessorExtensions
    {
        public static IServiceCollection AddPaymentProcessor(this IServiceCollection services)
        {
            services.AddScoped<IPaymentProcessorRepository, PaymentProcessorRepository>();            
            return services;
        }
    }
}

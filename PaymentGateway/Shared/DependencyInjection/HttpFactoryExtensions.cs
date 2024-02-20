using Application.Features.PaymentProcessor.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Shared.DependencyInjection
{
    public static class HttpFactoryExtensions
    {
        public static IServiceCollection AddHttpFactoryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IPaymentProcessorRepository, PaymentProcessorRepository>(
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7252/");
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("SecurityKey", configuration["AcquiringBank:ApiSecretKey"]);
                });

            return services;
        }
    }
}

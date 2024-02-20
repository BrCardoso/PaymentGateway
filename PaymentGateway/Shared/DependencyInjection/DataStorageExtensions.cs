using Microsoft.Extensions.DependencyInjection;
using Application.Shared.Repository;

namespace Application.Shared.DependencyInjection
{
    public static class DataStorageExtensions
    {
        public static IServiceCollection AddStorageExtension(this IServiceCollection services)
        {
            services.AddScoped<IDataStorageService, DataStorageService>();
            return services;
        }
    }
}

using CarSharing.Client.OrderApi;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;

namespace CarSharing.Dependency
{
    public static class ReastEaseDependencies
    {
        public static IServiceCollection AddOrderApi(this IServiceCollection services, string connectionString)
        {
            services.AddRestEaseClient<RentClient>(connectionString);
            return services;
        }

        public static IServiceCollection AddEmployeeApi(this IServiceCollection services, string connectionString)
        {
            return services;
        }
    }
}

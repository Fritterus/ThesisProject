using CarSharing.Client.BookApi;
using CarSharing.Client.CarApi;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;

namespace CarSharing.Dependency
{
    public static class ReastEaseDependencies
    {
        public static IServiceCollection AddBookApi(this IServiceCollection services, string connectionString)
        {
            services.AddRestEaseClient<IBookClient>(connectionString);
            return services;
        }

        public static IServiceCollection AddCarApi(this IServiceCollection services, string connectionString)
        {
            services.AddRestEaseClient<ICarClient>(connectionString);
            return services;
        }
    }
}

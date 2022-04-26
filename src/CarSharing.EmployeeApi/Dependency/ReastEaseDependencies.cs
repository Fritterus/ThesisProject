using CarSharing.EmployeeApi.Client.CarApi;
using RestEase.HttpClientFactory;

namespace CarSharing.Dependency
{
    public static class ReastEaseDependencies
    {
        public static IServiceCollection AddBookApi(this IServiceCollection services, string connectionString)
        {
            services.AddRestEaseClient<BookClient>(connectionString);
            return services;
        }
    }
}

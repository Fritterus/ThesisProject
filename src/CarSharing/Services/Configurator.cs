using CarSharing.Entities;
using CarSharing.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarSharing.Services
{
    public static class Configurator
    {   
        public static void ConfiguratorConfig(this IServiceCollection services, string connectionString)
        {
        }
    }
}

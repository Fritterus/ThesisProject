using CarSharing.DataLayer.Services;
using CarSharing.DataLayer.Services.Repository;
using CarSharing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarSharing.DataLayer.Dependency
{
    public static class DataLayerDependecies
    {
        public static IServiceCollection AddDataLayerDependecies(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<DbContext, Context>();
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<Context>();
            services.AddScoped<IRepository<Car>, Repository<Car>>();
            services.AddScoped<IRepository<Model>, Repository<Model>>();
            services.AddScoped<IRepository<Mark>, Repository<Mark>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Statement>, Repository<Statement>>();
            return services;
        }
    }
}

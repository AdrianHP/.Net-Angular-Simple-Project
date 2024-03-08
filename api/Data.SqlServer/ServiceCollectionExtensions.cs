using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(defaultConnectionString));

            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IDataDbContext, DataContext>();

            return services;
        }
    }
}
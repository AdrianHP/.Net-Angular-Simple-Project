using Data.Entities;
using Data.Interfaces;
using Data.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Services.Clases;
using Services.Interfaces;
using Services.Interfaces.CoreInterfaces;
using System.Text;
using System.Reflection;

namespace Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterProjectServices(this IServiceCollection services, IConfiguration configuration)
      
        {
            // Configure Identity
            services.AddIdentity<User, Role>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                //Other options go here
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

             services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // JWT configuration.
            var jwtConfig = configuration.GetSection("jwtConfig");
            var secretKey = jwtConfig["secret"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["validIssuer"],
                    ValidAudience = jwtConfig["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });


            //services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IDataCRUDService<IDataContext>, DataCRUDService<IDataContext>>();

            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString(
                "DefaultConnection"));
            });

            services.AddScoped<ITripRepository, TripRepository>();
            services.AddTransient<ICustomTripRepository, CustomTripRepository>();
            services.AddTransient<IAuthRespository, AuthRespository>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();
            return services;
        }


    }
}
using Core.Interface.Infrastructure.Cloudinary;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Infrastructure.CloudinaryServices;
using Infrastructure.Database;
using Infrastructure.Database.Cache;
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
            services.AddScoped<ICommunityTripRespository, CommunityTripRespository>();
            services.AddScoped<IAuthRespository, AuthRespository>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = config.GetConnectionString("Redis");
                redisOptions.Configuration = connection;
            });
            services.AddScoped<ICacheDbRespository, CacheDbRepository>();
            services.AddScoped(typeof(IPhotoRespository<>), typeof(PhotoRespository<>));
            services.AddScoped<IPhotoCloudinary, PhotoCloudinary>();
            return services;
        }


    }
}
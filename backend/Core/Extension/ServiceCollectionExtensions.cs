using Core.Interface.security;
using Core.Interface.Services;
using Core.security;
using Core.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreDependencyInjection(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                string[] hostnames = new string[] { "http://localhost:3000", "http://localhost:3001" };
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(hostnames);
                });
            });

            services.AddScoped<ITripService, TripService>();
            services.AddScoped<ICustomTripService, CustomTripService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();

            return services;
        }
    }
}
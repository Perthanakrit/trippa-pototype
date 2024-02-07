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
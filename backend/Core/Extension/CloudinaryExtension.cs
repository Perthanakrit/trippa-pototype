using Core.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extension
{
    public static class CloudinaryExtension
    {
        public static IServiceCollection AddCloudinaryExtension(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinaySettings>(config.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
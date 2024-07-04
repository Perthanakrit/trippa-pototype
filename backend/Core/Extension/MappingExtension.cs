using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extension
{
    public static class MappingExtension
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TripMapping).Assembly);
            services.AddAutoMapper(typeof(AuthServiceMapping).Assembly);
            services.AddAutoMapper(typeof(UserMapping).Assembly);
            services.AddAutoMapper(typeof(CommunityTripMapping).Assembly);
            return services;
        }
    }
}
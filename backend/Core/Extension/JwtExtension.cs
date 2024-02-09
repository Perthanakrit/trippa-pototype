using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Extension
{
    public static class JwtExtension
    {
        public static IServiceCollection AddJwtExtension(this IServiceCollection services, IConfiguration config)
        {
            JwtSettings jWTSettings = new();
            config.Bind(nameof(jWTSettings), jWTSettings);
            services.AddSingleton(jWTSettings);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(u =>
            {
                // u.RequireHttpsMetadata = false;
                // u.SaveToken = true;
                u.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTSettings.SecertKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(opt =>
            {
                var defaultAuthBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthBuilder = defaultAuthBuilder.RequireAuthenticatedUser();
                opt.DefaultPolicy = defaultAuthBuilder.Build();
            });

            return services;
        }
    }
}
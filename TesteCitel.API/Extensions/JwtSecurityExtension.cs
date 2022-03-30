using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using TesteCitel.API.Settings;

namespace TesteCitel.API.Extensions
{
    public static class JwtSecurityExtension
    {
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    LifetimeValidator = LifetimeValidator,
                    ValidateLifetime = true,
                };
            });
            return services;
        }

        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}

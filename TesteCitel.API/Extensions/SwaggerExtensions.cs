using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;

namespace TesteCitel.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteCitel.API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });

                SetXmlDocumentation(options);
            });
        }

        private static void SetXmlDocumentation(SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);

            if (existsXmlDocument)
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }

        private static string GetXmlDocumentPath()
        {
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = PlatformServices.Default.Application.ApplicationName;
            return Path.Combine(applicationBasePath, $"{applicationName}.xml");
        }
    }
}

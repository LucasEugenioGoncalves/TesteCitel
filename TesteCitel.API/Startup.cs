using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO.Compression;
using TesteCitel.API.Extensions;
using TesteCitel.API.Interfaces;
using TesteCitel.API.Models;
using TesteCitel.API.Services;
using TesteCitel.API.Settings;
using TesteCitel.Domain.Arguments.Category;
using TesteCitel.Domain.Arguments.Product;
using TesteCitel.Infra;
using TesteCitel.IoC;

namespace TesteCitel.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<bd_citelContext>(options => options
              .UseLazyLoadingProxies(false)
              .UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("8.0.28-mysql")));

            var jwtSettings = new JwtSettings();
            new ConfigureFromConfigurationOptions<JwtSettings>(
                Configuration.GetSection("JwtSettings"))
                    .Configure(jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddTransient<IServiceAccount, ServiceAccount>();
            services.AddTransient<IServiceJwt, ServiceJwt>();

            DependencyInjectionResolver.RegisterServices(services, Configuration);

            services.AddJwtSecurity(jwtSettings);
            services.AddSwagger();
            services.AddApiVersionings();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
              builder =>
              {
                  builder
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
                }));

            services.AddControllers();
            #region Configurações para compactar o retorno das requisições
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.Configure<GzipCompressionProviderOptions>(
                options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            #endregion
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProductViewModel, CreateProductRequest>();
                cfg.CreateMap<UpdateProductViewModel, UpdateProductRequest>();
                cfg.CreateMap<CreateCategoryViewModel, CreateCategoryRequest>();
                cfg.CreateMap<UpdateCategoryViewModel, UpdateCategoryRequest>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteCitel.API v1"));
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

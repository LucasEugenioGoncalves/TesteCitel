using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteCitel.Domain.Interfaces.Repositories;
using TesteCitel.Domain.Interfaces.Services;
using TesteCitel.Domain.Services;
using TesteCitel.Infra.Persistence.Repositories;
using TesteCitel.Infra.Transactions;

namespace TesteCitel.IoC
{
    public static class DependencyInjectionResolver
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        
            services.AddScoped<IServiceProduct, ServiceProduct>();
            services.AddScoped<IRepositoryProduct, RepositoryProduct>();

            services.AddScoped<IServiceCategory, ServiceCategory>();
            services.AddScoped<IRepositoryCategory, RepositoryCategory>();
        }
    }
}

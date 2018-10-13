using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Data.EntityFramework;
using Monitoring.Data.Repositories.Generic;
using Monitoring.Domain.Interfaces;

namespace Monitoring
{
    /// <summary>
    /// Менеджер сервисов
    /// </summary>
    public sealed class ServiceManager
    {
        private readonly IConfiguration _configuration;

        public ServiceManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Конфигурация сервисов
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(""));
            services.AddScoped<ITransactionManager, AppDbContextTransactionManager>();

            // Репозитории
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Data;
using Monitoring.Quartz;
using Monitoring.Services;
using Monitoring.Services.Impl;

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
        /// Конфигурирование сервисов
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(builder 
                => builder.UseNpgsql(_configuration.GetConnectionString("AppDb")));
            
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISiteInfoService, SiteInfoService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IScheduleJobService, ScheduleJobService>();

            services.AddSchedulerFactory();
            services.AddScheduler();
        }
    }
}

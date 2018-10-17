using System;
using Microsoft.EntityFrameworkCore;
using Monitoring.Domain.Entities;

namespace Monitoring.Data
{
    /// <summary>
    /// Контекст БД приложения
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Только для юнит тестов
        /// </summary>
        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Информация по сайтам
        /// </summary>
        public DbSet<SiteInfo> SiteInfos { get; set; }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Планируемые работы
        /// </summary>
        public DbSet<ScheduleJob> ScheduleJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<ScheduleJob>().HasIndex(x => x.Name).IsUnique();
        }
    }
}

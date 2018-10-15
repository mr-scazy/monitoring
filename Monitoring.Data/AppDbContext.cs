using Microsoft.EntityFrameworkCore;
using Monitoring.Domain.Entities;

namespace Monitoring.Data
{
    public class AppDbContext : DbContext
    {
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
    }
}

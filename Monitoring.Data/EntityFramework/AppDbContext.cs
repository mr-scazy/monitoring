using Microsoft.EntityFrameworkCore;

namespace Monitoring.Data.EntityFramework
{
    /// <summary>
    /// Контекст БД приложения
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

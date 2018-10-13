using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Data.EntityFramework
{
    /// <summary>
    /// Менеджер транзакции контекста БД приложения
    /// </summary>
    public class AppDbContextTransactionManager : ITransactionManager
    {
        private readonly DatabaseFacade _database;

        public AppDbContextTransactionManager(AppDbContext dbContext)
        {
            _database = dbContext.Database;
        }

        public ITransaction CurrenTransaction 
            => _database.CurrentTransaction != null 
                ? new AppDbContextTransaction(_database.CurrentTransaction) 
                : null;

        public ITransaction BeginTransaction()
            => new AppDbContextTransaction(_database.BeginTransaction());

        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
            => new AppDbContextTransaction(await _database.BeginTransactionAsync(cancellationToken));
    }
}

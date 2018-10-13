using Microsoft.EntityFrameworkCore.Storage;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Data.EntityFramework
{
    /// <summary>
    /// Транзакция контекста БД приложения
    /// </summary>
    internal class AppDbContextTransaction : ITransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        public AppDbContextTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public void Dispose() => _dbContextTransaction?.Dispose();

        public void Commit() => _dbContextTransaction?.Commit();

        public void Rollback() => _dbContextTransaction?.Rollback();
    }
}

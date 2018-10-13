using System.Threading;
using System.Threading.Tasks;

namespace Monitoring.Domain.Interfaces
{
    /// <summary>
    /// Менеджер транзакций
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Текущая транзакция
        /// </summary>
        ITransaction CurrenTransaction { get; }

        /// <summary>
        /// Начать транзакцию
        /// </summary>
        /// <returns>Новая транзакция</returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// Начать транзакцию
        /// </summary>
        /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
        /// <returns>Новая транзакция</returns>
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

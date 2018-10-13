using System;

namespace Monitoring.Domain.Interfaces
{
    /// <summary>
    /// Транзакция
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Совершить транзакцию
        /// </summary>
        void Commit();

        /// <summary>
        /// Откатить транзакцию
        /// </summary>
        void Rollback();
    }
}

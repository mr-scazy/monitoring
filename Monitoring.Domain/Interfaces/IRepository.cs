using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoring.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс обобщенного репозитория
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Запрос на получение всех записей
        /// </summary>
        IQueryable<T> GetAll();

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификато записи</param>
        T GetById(object id);

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификато записи</param>
        /// <param name="cancellationToken"></param>
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        void Create(T entity);

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        /// <param name="cancellationToken"></param>
        Task CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Создать записи
        /// </summary>
        /// <param name="entities">Сущности для добавления</param>
        void CreateRange(IEnumerable<T> entities);

        /// <summary>
        /// Создать записи
        /// </summary>
        /// <param name="entities">Сущности для добавления</param>
        /// <param name="cancellationToken"></param>
        Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        void Update(T entity);

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Обновить записи
        /// </summary>
        /// <param name="entities">Сущности для обновления</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Обновить записи
        /// </summary>
        /// <param name="entities">Сущности для обновления</param>
        /// <param name="cancellationToken"></param>
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи</param>
        void Delete(object id);

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Удалить записи
        /// </summary>
        /// <param name="ids">Идентификаторы удаляемых записей</param>
        void DeleteRange(IEnumerable<object> ids);

        /// <summary>
        /// Удалить записи
        /// </summary>
        /// <param name="ids">Идентификаторы удаляемых записей</param>
        /// <param name="cancellationToken"></param>
        Task DeleteRangeAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default(CancellationToken));
    }
}

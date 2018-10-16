using System.Collections.Generic;

namespace Monitoring.Domain.Dto
{
    /// <summary>
    /// Результат данных со списком
    /// </summary>
    public static class ListDataResult
    {
        /// <summary>
        /// Получить результат данных со списком
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка</typeparam>
        /// <param name="items">Список элементов</param>
        /// <param name="total">Общее количество элементов</param>
        public static ListDataResult<TItem> Result<TItem>(IReadOnlyList<TItem> items, int total)
            => new ListDataResult<TItem>(items, total);
    }

    /// <summary>
    /// Результат данных со списком
    /// </summary>
    /// <typeparam name="T">Тип элемента списка</typeparam>
    public class ListDataResult<T>
    {
        /// <summary>
        /// Результат данных со списком
        /// </summary>
        /// <param name="items">Список элементов</param>
        /// <param name="total">Общее количество элементов</param>
        public ListDataResult(IReadOnlyList<T> items, int total)
        {
            Items = items;
            Total = total;
        }

        /// <summary>
        /// Список элементов
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public int Total { get; }
    }
}

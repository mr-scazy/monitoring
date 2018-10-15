using System.Collections.Generic;

namespace Monitoring.Domain.Dto
{
    public static class ListDataResult
    {
        public static ListDataResult<TItem> NewResult<TItem>(IReadOnlyList<TItem> items, int total)
            => new ListDataResult<TItem>(items, total);
    }

    public class ListDataResult<T>
    {
        public ListDataResult(IReadOnlyList<T> items, int total)
        {
            Items = items;
            Total = total;
        }

        public IReadOnlyList<T> Items { get; }

        public int Total { get; }
    }
}

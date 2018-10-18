using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.IntegrationTests.Dto
{
    public class ListData<T>
    {
        /// <summary>
        /// Список элементов
        /// </summary>
        public T[] Items { get; set; }

        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public int Total { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace StudentManagerBackEnd.DataAccess
{
    public class PaginatedResult<T> : IEnumerable<T>
    {
        public PaginatedResult(int page, int total, IEnumerable<T> items)
        {
            this.Items = items;
            this.Total = total;
            this.Page = page;
        }

        public int Total { get; }

        public int Page { get; }

        public IEnumerable<T> Items { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }
    }
}

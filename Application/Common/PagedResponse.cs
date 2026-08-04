using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class PagedResponse<T>
    {
        public IReadOnlyCollection<T> Items { get; init; } = [];

    public int PageNumber { get; init; }

        public int PageSize { get; init; }

        public int TotalRecords { get; init; }

        public int TotalPages { get; init; }
    }
}

﻿using XO.Business.Interfaces.Paginated;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XO.Business.Paginated
{
    [JsonObject]
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;

            Clear();
            AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public IEnumerable<T> Items => this.Select(c => c);

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public int TotalItems { get; private set; }
        public int Result { get; }
        public IEnumerable<T> Records => this.Select(c => c).ToList();
        public Exception Ex { get; private set; }
    }
}

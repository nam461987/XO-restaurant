using XO.Business.Interfaces.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Business.Interfaces.Paginated
{
    public interface IPaginatedList<T> : IList<T>, IResponseObject<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        int TotalItems { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}

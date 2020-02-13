using application.interfaces.mapping;
using System.Collections.Generic;
using System.Text;

namespace application.interfaces.paging
{
    public interface IPagedResult<T> where T : IHaveCustomMapping
    {
        IEnumerable<T> Data { get; }
        PagingInfo Paging { get; }

    }

    public struct PagingInfo
    {
        public int PageNo { get; internal set; }

        public int PageSize { get; internal set; }

        public int PageCount { get; internal set; }

        public long TotalRecordCount { get; set; }

    }
}

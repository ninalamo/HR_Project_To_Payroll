using application.interfaces.paging;
using domain;
using System.Collections.Generic;
using System.Linq;

namespace application.cqrs._base
{
    public class PagedQueryRequestBase : IPagedQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; internal set; }

        public int GetSkip()
        {
            if (PageNumber <= 0) PageNumber = 1;

            if (PageSize < 10) PageSize = 10;

            return (PageNumber - 1) * PageSize;
        }


    }
}

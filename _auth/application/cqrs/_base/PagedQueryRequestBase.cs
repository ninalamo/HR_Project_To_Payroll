using application.interfaces.paging;
using domain;
using System.Collections.Generic;
using System.Linq;

namespace application.cqrs._base
{
    public class PagedQueryRequestBase<Tidentity> : IPagedQuery<Tidentity> where Tidentity : struct
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public void SetSkipAndTotalCount(out int skip, out int total, IEnumerable<BaseAudit<Tidentity>> list)
        {
            if (PageNumber <= 0) PageNumber = 1;

            if (PageSize < 10) PageSize = 10;

            skip = (PageNumber - 1) * PageSize;
            total = list.Count();
        }
    }
}

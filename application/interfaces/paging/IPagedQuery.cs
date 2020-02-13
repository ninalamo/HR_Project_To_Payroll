using domain;
using System.Collections.Generic;

namespace application.interfaces.paging
{
    public interface IPagedQuery<Tidentity> where Tidentity : struct
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }

        void SetSkipAndTotalCount(out int skip, out int total, IEnumerable<BaseAudit<Tidentity>> list);
    }
}

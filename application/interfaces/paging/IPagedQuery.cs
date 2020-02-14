using domain;
using System.Collections.Generic;

namespace application.interfaces.paging
{
    public interface IPagedQuery
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int Total { get; }

        int GetSkip();


    }
}

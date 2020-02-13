using application.interfaces.mapping;
using application.interfaces.paging;
using System;
using System.Collections.Generic;

namespace application.cqrs._base
{
    public abstract class PagedQueryResponseBase<Tidentity> : IPagedResult<Tidentity> where Tidentity :  IHaveCustomMapping
    {
        public IEnumerable<Tidentity> Data { get; private set; }

        public PagingInfo Paging { get; set; }

        protected PagedQueryResponseBase(IEnumerable<Tidentity> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Data = items;
            Paging = new PagingInfo
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecordCount = totalRecordCount,
                PageCount = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0,
            };
        }
    }
}

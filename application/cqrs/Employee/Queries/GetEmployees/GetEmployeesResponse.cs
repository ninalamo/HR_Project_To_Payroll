using application.cqrs._base;
using application.interfaces.mapping;
using AutoMapper;
using System.Collections.Generic;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeesResponse : PagedQueryResponseBase<GetEmployeesDto>
    {
        public GetEmployeesResponse(IEnumerable<GetEmployeesDto> items, int pageNo, int pageSize, long totalRecordCount) : base(items, pageNo, pageSize, totalRecordCount)
        {
        }
    }

    public class GetEmployeesDto : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            throw new System.NotImplementedException();
        }
    }
}
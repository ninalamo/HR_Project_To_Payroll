using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeesRequestHandler : RequestHandlerBase, IRequestHandler<GetEmployeesRequest, GetEmployeesResponse>
    {
        public GetEmployeesRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetEmployeesResponse> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
        {
            var total = dbContext.Employees.Count();
            var skip = request.GetSkip();
            var list = await dbContext.Employees.AsNoTracking().Skip(skip).Take(request.PageSize).ProjectTo<GetEmployeesDto>(mapper.ConfigurationProvider).ToArrayAsync();

            return new GetEmployeesResponse(list, request.PageNumber, request.PageSize, total);
        }
    }
}

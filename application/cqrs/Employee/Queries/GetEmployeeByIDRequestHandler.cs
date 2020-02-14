using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByIDRequestHandler : RequestHandlerBase, IRequestHandler<GetEmployeeByIDRequest, GetEmployeeByIDResponse>
    {
        public GetEmployeeByIDRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetEmployeeByIDResponse> Handle(GetEmployeeByIDRequest request, CancellationToken cancellationToken)
        {
            return GetEmployeeByIDResponse.Create(await dbContext.Employees.FindAsync(request.EmployeeID));
        }
    }
}

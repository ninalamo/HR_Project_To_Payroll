using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByEmail_RequestHandler : RequestHandlerBase, IRequestHandler<GetEmployeeByEmail_Request, GetEmployeeByEmail_Response>
    {
        public GetEmployeeByEmail_RequestHandler(IApplicationDbContext dbContext, IMapper mapper):base(dbContext, mapper)
        {

        }

        public async Task<GetEmployeeByEmail_Response> Handle(GetEmployeeByEmail_Request request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(i => i.CompanyEmail.ToLower() == request.CompanyEmail.ToLower());

            if (employee == null) return null;//throw new NotFoundException(nameof(domain.Employee), request.CompanyEmail);

            return GetEmployeeByEmail_Response.Create(employee);
        }
    }
}

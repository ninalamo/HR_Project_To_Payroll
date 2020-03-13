using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Commands
{
    public class CreateEmployee_RequestHandler : RequestHandlerBase, IRequestHandler<CreateEmployee_Request, CreateEmployee_Response>
    {
        public CreateEmployee_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CreateEmployee_Response> Handle(CreateEmployee_Request request, CancellationToken cancellationToken)
        {

            //validate if reportsTo exists

            var boss = dbContext.Approvers.Include(i=> i.Employee)
                .FirstOrDefault(i => i.ID == request.ApproverID);

            if (boss == null && request.ApproverID != 0) throw new NotFoundException(nameof(domain.Employee), request.ApproverID);

            var employee = new domain.Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmployeeNumber = request.EmployeeNumber,
                IsActive =true,
                CompanyEmail = request.CompanyEmail,
                PersonalEmail = request.PersonalEmail,
                CanApprove = request.CanApprove,
                ReportsTo = boss != null ? boss.Employee.CompanyEmail : "",
            };

            Blame(employee, request.CreatedBy);

            dbContext.Employees.Add(employee);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateEmployee_Response(nameof(Employee), employee.ID);
        }
    }
}

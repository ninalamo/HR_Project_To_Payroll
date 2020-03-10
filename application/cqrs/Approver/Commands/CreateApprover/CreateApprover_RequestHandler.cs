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

namespace HR.Application.cqrs.Approver.Commands
{
    public class CreateApprover_RequestHandler : RequestHandlerBase, IRequestHandler<CreateApprover_Request>
    {
        public CreateApprover_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CreateApprover_Request request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(i => i.CompanyEmail.ToLower() == request.CompanyEmail.ToLower());

            if (employee == null) throw new NotFoundException(nameof(domain.Employee), request.CompanyEmail);

            var approver = new domain.Approver
            {
                EmployeeID = employee.ID,
                IsActive = true,
                Level = request.Level,
                TypeOfRequest = request.TypeOfRequest
            };

            Blame(approver, request.CreatedBy);

            dbContext.Approvers.Add(approver);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        
    }
}

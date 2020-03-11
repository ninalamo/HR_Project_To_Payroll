using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Request.Commands
{
    public class CreateOverTimeRequest_RequestHandler : RequestHandlerBase, IRequestHandler<CreateOverTimeRequest_Request>
    {
        public CreateOverTimeRequest_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CreateOverTimeRequest_Request request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(i => i.CompanyEmail.ToLower() == request.Requestor.ToLower());

            if (employee == null) 
                throw new NotFoundException(nameof(domain.RequestTracker), request.Requestor);

            if (string.IsNullOrWhiteSpace(employee.ReportsTo)) 
                throw new Exception("Cannot process. User must be assigned a supervisor.");

            var approvers = await dbContext.Approvers.Include(i => i.Employee).AsNoTracking().ToArrayAsync();

            if (!approvers.ToList().Any()) 
                throw new Exception("Cannot process. Cannot find approver.");

            var supervisor = approvers.FirstOrDefault(i => i.Employee.CompanyEmail.ToLower() == employee.ReportsTo.ToLower());

            var limit = approvers.Where(i => i.TypeOfRequest == domain.RequestType.Overtime).Max(i => i.Level);

            var finalApprover = approvers.FirstOrDefault(i => i.TypeOfRequest == domain.RequestType.Overtime && i.Level == limit);

            var requestTracker = new RequestTracker
            {
                RequestorID = employee.ID,
                IsActive = true,
                TypeOfRequest = domain.RequestType.Overtime,
            };

            requestTracker.ApproverList.Add(new ApprovalTracker
            {
                ApproverID = supervisor.ID,
                IsActive = true,
                Status = null,
                CreatedBy = request.Requestor,
                ModifiedBy = request.Requestor
            });

            requestTracker.ApproverList.Add(new ApprovalTracker
            {
                ApproverID = finalApprover.ID,
                IsActive = true,
                Status = null,
                CreatedBy = request.Requestor,
                ModifiedBy = request.Requestor
            });

            var otRequest = new OverTimeRequest
            {
                Classification = request.Classification,
                EmployeeID = employee.ID,
                IsActive = true,
                Purpose = request.Purpose,
                ShiftDate = request.ShiftDate,
                TimeStart = request.TimeStart,
                TimeEnd = request.TimeEnd,
                Tracker = requestTracker

            };

            dbContext.OverTimeRequests.Add(otRequest);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

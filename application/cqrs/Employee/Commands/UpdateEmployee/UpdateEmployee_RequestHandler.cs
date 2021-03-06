﻿using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Commands
{
    public class UpdateEmployee_RequestHandler : RequestHandlerBase, IRequestHandler<UpdateEmployee_Request, UpdateEmployee_Response>
    {
        public UpdateEmployee_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<UpdateEmployee_Response> Handle(UpdateEmployee_Request request, CancellationToken cancellationToken)
        {
            var boss = await dbContext.Approvers.Include(i => i.Employee).FirstOrDefaultAsync(i => i.ID == request.ReportsTo);

            var employee = await dbContext.Employees.FindAsync(request.EmployeeID);
            employee.EmployeeNumber = request.EmployeeNumber;
            employee.CompanyEmail = request.CompanyEmail;
            employee.PersonalEmail = request.PersonalEmail;
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.IsActive = request.IsActive;
            employee.ReportsTo = boss == null ? "" : boss.Employee.CompanyEmail ;
            employee.CanApprove = request.CanApprove;

            Blame(employee, employee.CreatedBy, request.ModifiedBy);

            dbContext.Employees.Update(employee);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateEmployee_Response(nameof(domain.Employee), employee.ID);

        }
    }
}

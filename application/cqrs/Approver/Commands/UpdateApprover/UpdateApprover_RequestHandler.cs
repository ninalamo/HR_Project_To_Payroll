using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Approver.Commands
{
    public class UpdateApprover_RequestHandler : RequestHandlerBase, IRequestHandler<UpdateApprover_Request>
    {
        public UpdateApprover_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(UpdateApprover_Request request, CancellationToken cancellationToken)
        {
            var approver = await dbContext.Approvers.FindAsync(request.ApproverID);

            if (approver == null) throw new NotFoundException(nameof(domain.Approver), request.ApproverID);

            approver.IsActive = request.IsActive;
            approver.ModifiedBy = request.ModifiedBy;
            approver.Level = request.Level;
            approver.TypeOfRequest = request.TypeOfRequest;

            dbContext.Approvers.Update(approver);

            await dbContext.SaveChangesAsync(cancellationToken);

            //TODO: update all request that are approved by these guy

            return Unit.Value;
        }
    }
}

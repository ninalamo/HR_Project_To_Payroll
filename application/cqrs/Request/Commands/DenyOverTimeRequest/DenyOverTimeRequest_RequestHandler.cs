using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using domain;
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
    public class DenyOverTimeRequest_RequestHandler : RequestHandlerBase, IRequestHandler<DenyOverTimeRequest_Request>
    {
        public DenyOverTimeRequest_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(DenyOverTimeRequest_Request request, CancellationToken cancellationToken)
        {
            var otRequest = await dbContext.OverTimeRequests
                .Include(i => i.Tracker)
                    .ThenInclude(i => i.Requestor)
                .Include(i => i.Tracker)
                    .ThenInclude(i => i.ApproverList)
                .FirstOrDefaultAsync(i => i.ID == request.OTRequestID);

            if (otRequest == null) throw new NotFoundException(nameof(OverTimeRequest), request.OTRequestID);

            if (otRequest.Tracker.IsCancelled)
            {
                throw new Exception("Request has already been cancelled.");
            }

            if(otRequest.Tracker.ApproverList.All(i => i.Status.HasValue))
            {
                throw new Exception("Request has already been completed.");
            }

            var forYou = otRequest.Tracker.ApproverList.FirstOrDefault(i => i.ApproverEmail.ToLower() == request.ApproverEmail.ToLower());

            if (forYou == null) throw new NotFoundException(nameof(domain.ApprovalTracker), request.ApproverEmail);

            Blame(otRequest, request.ApproverEmail);
            Blame(otRequest.Tracker, request.ApproverEmail);
            Blame(forYou, request.ApproverEmail);

            forYou.Status = false;
            forYou.Note = request.Note;

            dbContext.OverTimeRequests.Update(otRequest);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}

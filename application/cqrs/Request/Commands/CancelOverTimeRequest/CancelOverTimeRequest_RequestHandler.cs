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

namespace HR.Application.cqrs.Request.Commands
{
    public class CancelOverTimeRequest_RequestHandler : RequestHandlerBase, IRequestHandler<CancelOverTimeRequest_Request>
    {
        public CancelOverTimeRequest_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CancelOverTimeRequest_Request request, CancellationToken cancellationToken)
        {
            var otRequest = await dbContext.OverTimeRequests
                .Include(i => i.Tracker)
                    .ThenInclude(i => i.Requestor)
                .Include(i => i.Tracker)
                    .ThenInclude(i => i.ApproverList)
                .FirstOrDefaultAsync(i => i.ID == request.OTRequestID);

            if (otRequest == null) throw new NotFoundException(nameof(OverTimeRequest), request.OTRequestID);

            if (otRequest.Tracker.IsCancelled)
                throw new Exception("Request has already been cancelled.");

            if(otRequest.Tracker.ApproverList.All(i => i.Status.HasValue))
                throw new Exception("Request has already been completed.");

            if (otRequest.Tracker.Requestor.CompanyEmail.ToLower() != request.RequestorEmail.ToLower())
                throw new Exception("Only requestor can cancel a request.");

            Blame(otRequest, request.RequestorEmail);
            Blame(otRequest.Tracker, request.RequestorEmail);

            otRequest.Tracker.IsCancelled = true;
            otRequest.Tracker.ModifiedBy = request.RequestorEmail;
            otRequest.Tracker.ApproverList.ToList().ForEach(i => {
                i.Status = null;
                i.ModifiedBy = request.RequestorEmail;
                i.Note = request.Note;
                Blame(i, request.RequestorEmail);
            });

            dbContext.OverTimeRequests.Update(otRequest);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}

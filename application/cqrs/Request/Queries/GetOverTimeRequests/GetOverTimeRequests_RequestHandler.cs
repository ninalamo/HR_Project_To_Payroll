using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Request.Queries
{
    public class GetOverTimeRequests_RequestHandler : RequestHandlerBase, IRequestHandler<GetOverTimeRequests_Request, GetOverTimeRequests_Response>
    {
        public GetOverTimeRequests_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetOverTimeRequests_Response> Handle(GetOverTimeRequests_Request request, CancellationToken cancellationToken)
        {
            //check first if employee ( if not NULL ) is an approver
            if (request.EmployeeID.HasValue)
            {
                var approver = await dbContext.Approvers.Include(i => i.Employee).FirstOrDefaultAsync(i => i.EmployeeID == request.EmployeeID.Value);

                if (approver == null) 
                    throw new NotFoundException(nameof(domain.Approver), request.EmployeeID.Value);

                //fetch all requests pending this person approval
                var requests = dbContext.OverTimeRequests
                    .Include(i => i.Tracker)
                    .ThenInclude(i => i.Requestor)
                    .AsNoTracking()
                    .Where(i => i.Tracker.TypeOfRequest == domain.RequestType.Overtime
                        && i.Tracker.ApproverList.Any(i => i.ApproverID == approver.ID
                            && i.Status == null))
                    .OrderByDescending(i => i.ModifiedOn);

                return new GetOverTimeRequests_Response{
                    Result = await requests.ProjectTo<GetOverTimeRequestsResponseDto>(mapper.ConfigurationProvider).ToListAsync()
                };
            }
           
            var result2 = dbContext.OverTimeRequests.
                    Include(i => i.Tracker)
                    .AsNoTracking()
                    .Where(i => i.Tracker.TypeOfRequest == domain.RequestType.Overtime
                        && i.Tracker.ApproverList.Any(i => i.Status == null))
                    .OrderByDescending(i => i.ModifiedOn);

            return new GetOverTimeRequests_Response
            {
                Result = await result2.ProjectTo<GetOverTimeRequestsResponseDto>(mapper.ConfigurationProvider).ToListAsync()
            };

        }
    }
}

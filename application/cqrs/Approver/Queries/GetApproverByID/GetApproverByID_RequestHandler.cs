using application.cqrs._base;
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

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApproverByID_RequestHandler : RequestHandlerBase, IRequestHandler<GetApproverByID_Request, GetApproverByID_Response>
    {
        public GetApproverByID_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetApproverByID_Response> Handle(GetApproverByID_Request request, CancellationToken cancellationToken)
        {
            var result = dbContext.Approvers.Include(i => i.Employee).AsNoTracking().Where(i => i.ID == request.ApproverID);

            return new GetApproverByID_Response
            {
                Result = await result.ProjectTo<GetApproverByID_Dto>(mapper.ConfigurationProvider).FirstOrDefaultAsync()
            };
        }
    }
}

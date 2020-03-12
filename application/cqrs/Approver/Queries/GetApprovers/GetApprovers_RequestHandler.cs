using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApprovers_RequestHandler : RequestHandlerBase, IRequestHandler<GetApprovers_Request, GetApprovers_Response>
    {
        public GetApprovers_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetApprovers_Response> Handle(GetApprovers_Request request, CancellationToken cancellationToken)
        {
            var result = dbContext.Approvers.Include(i => i.Employee).AsNoTracking();

            return new GetApprovers_Response
            {
                Result = await result.ProjectTo<GetApprovers_Dto>(mapper.ConfigurationProvider).ToArrayAsync()
            };
        }
    }
}

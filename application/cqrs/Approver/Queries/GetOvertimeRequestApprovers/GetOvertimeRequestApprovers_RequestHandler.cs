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
    public class GetOvertimeRequestApprovers_RequestHandler : RequestHandlerBase, IRequestHandler<GetOvertimeRequestApprovers_Request, GetOvertimeRequestApprovers_Response>
    {
        public GetOvertimeRequestApprovers_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetOvertimeRequestApprovers_Response> Handle(GetOvertimeRequestApprovers_Request request, CancellationToken cancellationToken)
        {
            var result = dbContext.Approvers.Include(i => i.Employee).AsNoTracking().Where(i => i.TypeOfRequest == domain.RequestType.Overtime);

            return new GetOvertimeRequestApprovers_Response
            {
                Approvers = await result.ProjectTo<GetOvertimeApprovers_Dto>(mapper.ConfigurationProvider).ToArrayAsync()
            };
        }
    }
}

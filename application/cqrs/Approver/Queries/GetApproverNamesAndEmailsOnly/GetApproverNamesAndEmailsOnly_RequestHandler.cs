using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApproverNamesAndEmailsOnly_RequestHandler : RequestHandlerBase, IRequestHandler<GetApproverNamesAndEmailsOnly_Request, GetApproverNamesAndEmailsOnly_Response>
    {
        public GetApproverNamesAndEmailsOnly_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetApproverNamesAndEmailsOnly_Response> Handle(GetApproverNamesAndEmailsOnly_Request request, CancellationToken cancellationToken)
        {
            var result = dbContext.Approvers.Include(i => i.Employee).AsNoTracking().OrderBy(i => i.Employee.CompanyEmail).ThenBy(i => i.Employee.LastName).ThenBy(i => i.Employee.FirstName);

            var temp = await result.ProjectTo<GetApproverNamesAndEmailsOnly_Dto>(mapper.ConfigurationProvider).ToListAsync();

            temp.Insert(0, new GetApproverNamesAndEmailsOnly_Dto { ApproverID = 0, FullName = "N/A" , DisplayName = "N/A" });

            return new GetApproverNamesAndEmailsOnly_Response
            {
                Result = temp.OrderBy(i => i.ApproverID)
            };
        }
    }
}

using MediatR;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApproverNamesAndEmailsOnly_Request : IRequest<GetApproverNamesAndEmailsOnly_Response>
    {
    }
}
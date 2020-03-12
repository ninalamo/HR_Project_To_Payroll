using MediatR;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApproverByID_Request : IRequest<GetApproverByID_Response>
    {
        public long ApproverID { get; set; }
    }
}
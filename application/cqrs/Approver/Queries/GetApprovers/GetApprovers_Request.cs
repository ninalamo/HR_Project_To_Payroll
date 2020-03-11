using MediatR;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApprovers_Request : IRequest<GetApprovers_Response>
    {
    }
}